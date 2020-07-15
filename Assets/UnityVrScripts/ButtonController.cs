using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace UnityVRScripts {
    public class ButtonController : XRBaseInteractable {
        public UnityEvent OnPress = null;
        public ArdCom ArdComManager = null;
        public DummyCubeSpawner CubeDummySpawner;

        private float yMin = 0.0f;
        private float yMax = 0.0f;
        private bool previousPress = false;

        private XRBaseInteractor hoverInteractor = null;
        private float previousHandHeight = 0.0f;

        protected override void Awake() {
            base.Awake();
            onHoverEnter.AddListener(StartPress);
            onHoverExit.AddListener(EndPress);
        }

        private void OnDestroy() {
            onHoverEnter.RemoveListener(StartPress);
            onHoverExit.RemoveListener(EndPress);
        }

        private void StartPress(XRBaseInteractor interactor) {
            hoverInteractor = interactor;
            previousHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
            ArdComManager.sendToArduino("on");
        }

        private void EndPress(XRBaseInteractor interactor) {
            hoverInteractor = null;
            previousHandHeight = 0.0f;

            previousPress = false;
            SetYPosition(yMax);
            ArdComManager.sendToArduino("off");
        }

        private void Start() {
            SetMinMax();
        }

        private void SetMinMax() {
            Collider collider = GetComponent<Collider>();
            yMin = transform.localPosition.y - (collider.bounds.size.y * 0.5f);
            yMax = transform.localPosition.y;
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase) {
            if (hoverInteractor) {
                float newHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
                float handDiff = previousHandHeight - newHandHeight;
                previousHandHeight = newHandHeight;

                float newPosition = transform.localPosition.y - handDiff;
                SetYPosition(newPosition);

                CheckPress();
            }
        }


        private float GetLocalYPosition(Vector3 position) {
            Vector3 localPosition = transform.root.InverseTransformPoint(position);

            return localPosition.y;
        }

        private void SetYPosition(float position) {
            Vector3 newPosition = transform.localPosition;
            newPosition.y = Mathf.Clamp(position, yMin, yMax);
            transform.localPosition = newPosition;
        }

        private void CheckPress() {
            bool inPosition = InPosition();

            if (inPosition && inPosition != previousPress) {
                OnPress.Invoke();
            }

            previousPress = inPosition;
        }

        private bool InPosition() {
            float inRange = Mathf.Clamp(transform.localPosition.y, yMin, yMax + 0.01f);

            return transform.localPosition.y == inRange;
        }

        public void ButtonPressed() {
            CubeDummySpawner.GameCubeSpawner();
        }
    }
}

