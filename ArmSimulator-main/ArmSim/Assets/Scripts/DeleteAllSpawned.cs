using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAllSpawned : MonoBehaviour
{
     private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            GameObject[] spawnedCubes = GameObject.FindGameObjectsWithTag("SpawnedCube");
            GameObject[] spawnedRectangles = GameObject.FindGameObjectsWithTag("SpawnedRectangle");
            for(int i = 0; i < spawnedCubes.Length; i++){
                Destroy(spawnedCubes[i]);
            }
            for(int i = 0; i < spawnedRectangles.Length; i++){
                Destroy(spawnedRectangles[i]);
            }

        }
    }
}
