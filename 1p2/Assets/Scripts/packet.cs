using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class packet : MonoBehaviour
{
    public float DefaultSpeed = 2.5f;

    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    //fixed update is needed, so that the movement created by transforming position is not laggy
    void FixedUpdate()
    {
        //moves the gameObject forward with given speed
        transform.position = transform.position + new Vector3(0.005f * Speed, 0, 0);

        if (Input.GetKey(KeyCode.UpArrow)){
            body.gravityScale = -1;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            body.gravityScale = 1;
        }
    }
}
