using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleSpawner : MonoBehaviour
{
    public GameObject SpawnRectangle;
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
            GameObject newRectangle = Instantiate(SpawnRectangle, pos, rot);
            newRectangle.tag = "SpawnedRectangle";
        }
    }
}
