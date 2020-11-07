﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketMovement : MonoBehaviour
{
    public float speed;
    public float DefaultSpeed = 2.5f;
    public GameObject BodySprite;
    
    private Rigidbody2D body;
    private bool pause = false;

    // Start is called before the first frame update
    void Start() {
        speed = DefaultSpeed;
        body = transform.GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<CameraMovement>().setPacketGO(transform);
    }

    public void SetSpeed(float speed) {
        this.speed = speed;
    }

    // Update is called once per frame
    //fixed update is needed, so that the movement created by transforming position is not laggy
    void FixedUpdate()
    {
        //moves the gameObject forward with given speed
        transform.position = transform.position + new Vector3(0.005f * DefaultSpeed, 0, 0);
        
        Vector3 prevPosition = BodySprite.transform.position;
        prevPosition.y += Mathf.Sin(Time.time * 10) * 0.1f;
        BodySprite.transform.SetPositionAndRotation(prevPosition, BodySprite.transform.rotation);

        if (!pause) {
            if (Input.GetKey(KeyCode.Space))
            {
                body.gravityScale = body.gravityScale * -1;
                pause = true;
                StopAllCoroutines();
                StartCoroutine("WaitForPause");
            }
            else if (Input.GetKey(KeyCode.UpArrow)){
                body.gravityScale = Math.Abs(body.gravityScale) * -1;
            }

            else if (Input.GetKey(KeyCode.DownArrow))
            {
                body.gravityScale = Math.Abs(body.gravityScale);
            }
        }
    }
    
    IEnumerator WaitForPause()
    {
        yield return new WaitForSeconds(0.2f);
        pause = false;
    }
}
