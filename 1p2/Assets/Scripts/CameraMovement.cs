using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    
    private Transform playerTransform;        //Public variable to store a reference to the player game object
    private float offset = 3;
    private float maxXoff = 3;
    private Camera thisCamera;

    // Use this for initialization
    void Start () {
        thisCamera = GetComponent<Camera>();
        // do this in Run method that will be called on clicking play
    }

    public void setPacketGO(Transform transfrom) {
        playerTransform = transfrom;
    }

    // LateUpdate is called after Update each frame
    void FixedUpdate () {
        if (playerTransform) {
            var playerPos = playerTransform.position;
            var idealPosition = new Vector3(playerPos.x + offset, playerPos.y + 4, transform.position.z);
            var yDistance = idealPosition.y - transform.position.y;
        
            transform.position = new Vector3(idealPosition.x, transform.position.y + 0.05f * yDistance, idealPosition.z);
        }
    }
}