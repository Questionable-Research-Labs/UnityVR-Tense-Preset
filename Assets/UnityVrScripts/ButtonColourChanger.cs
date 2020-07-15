using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace UnityVRScripts {
    public class ButtonColourChanger : MonoBehaviour {
        public Material selectedMeterial = null;

        private MeshRenderer meshRenderer = null;
        private XRBaseInteractable interactable = null;
        private Material originalMaterial = null;

        private void Awake() {
            meshRenderer = GetComponent<MeshRenderer>();
            originalMaterial = meshRenderer.material;

            interactable = GetComponent<XRBaseInteractable>();
            interactable.onHoverEnter.AddListener(SetSelectedMaterial);
            interactable.onHoverExit.AddListener(SetOriginalMaterial);
        }

        private void OnDestroy() {
            interactable.onHoverEnter.RemoveListener(SetSelectedMaterial);
            interactable.onHoverExit.RemoveListener(SetOriginalMaterial);
        }

        private void SetSelectedMaterial(XRBaseInteractor interactor) {
            Debug.Log("Changing Button to selected");
            meshRenderer.material = selectedMeterial;
        }
        private void SetOriginalMaterial(XRBaseInteractor interactor) {
            meshRenderer.material = originalMaterial;
        }
    }
}
