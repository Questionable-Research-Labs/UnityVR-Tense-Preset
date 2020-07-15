using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityVRScripts {
    public class DummyCubeSpawner : MonoBehaviour {
        public GameObject dummyCube;
        public int amountToSpawn;
        public float startingHeight;


        public void GameCubeSpawner() {
            for (var i = 0; i < amountToSpawn; i++) {
                Instantiate(dummyCube, new Vector3(0.0f, startingHeight + (i * 0.5f), 0.0f), Quaternion.identity);
            }


        }
    }
}
