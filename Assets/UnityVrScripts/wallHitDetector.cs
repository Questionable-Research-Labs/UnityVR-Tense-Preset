using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityVRScripts {
    public class wallHitDetector : MonoBehaviour {
        // Start is called before the first frame update
        public ArdCom ardComManager;

        void OnCollisionEnter(Collision collision) {
            var tag = collision.gameObject.tag;
            if (tag == "RightController") {
                ardComManager.RightRelayOn();
            }
            else if (tag == "LeftController") {
                ardComManager.LeftRelayOn();
            }
            else {
                Debug.Log("Collision detected but not correct tag");
            }
        }
        void OnCollisionExit(Collision collision) {
            var tag = collision.gameObject.tag;
            if (tag == "RightController") {
                ardComManager.RightRelayOff();
            }
            else if (tag == "LeftController") {
                ardComManager.LeftRelayOff();
            }
            else {
                Debug.Log("Collision detected but not correct tag");
            }
        }
    }
}
