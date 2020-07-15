using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace UnityVRScripts {
    public class DummyCubeInteraction : MonoBehaviour {
        public Material defaultMaterial = null;
        public Material activatedMaterial = null;
        public Material highlightedMaterial = null;

        private MeshRenderer meshRenderer = null;
        private XRGrabInteractable grabInteractable = null;
        private void Awake() {
            meshRenderer = GetComponent<MeshRenderer>();
            grabInteractable = GetComponent<XRGrabInteractable>();
            
            grabInteractable.onActivate.AddListener(ActivateCube);
            grabInteractable.onDeactivate.AddListener(DefaultCube);
            
            grabInteractable.onHoverEnter.AddListener(HighlightCube);
            grabInteractable.onHoverExit.AddListener(DefaultCube);
        }

        private void OnDestroy() {
            grabInteractable.onActivate.RemoveListener(ActivateCube);
            grabInteractable.onDeactivate.RemoveListener(DefaultCube);
        }

        private void ActivateCube(XRBaseInteractor interactor) {
            meshRenderer.material = activatedMaterial;
            
        }

        private void DefaultCube(XRBaseInteractor interactor) {
            meshRenderer.material = defaultMaterial;
        }

        private void HighlightCube(XRBaseInteractor interactor) {
            meshRenderer.material = highlightedMaterial;
        }
    }
}