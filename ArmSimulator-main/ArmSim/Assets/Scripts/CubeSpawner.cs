using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject SpawnCube;
    public Transform SpawnPosition;
    private Quaternion rot;
    private Vector3 pos;
    

    private void Start() {
        rot = SpawnPosition.rotation;
        pos = SpawnPosition.position;
        pos.y = pos.y + 5;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            GameObject newCube = Instantiate(SpawnCube, pos, rot);
            newCube.tag = "SpawnedCube";
        }
    }
}
