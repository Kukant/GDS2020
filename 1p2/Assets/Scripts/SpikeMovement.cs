using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMovement : MonoBehaviour
{
    public float Speed;
    public bool X;
    //moves in x or y position 
    public float UpperBoundary;
    public float LowerBoundary;

    private bool up = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (X)
        {
            MoveX();
        }
        else
        {
            MoveY();
        }
    }

    private void MoveX()
    {
        float newX;
        if (up)
        {
            newX = transform.position.x - 0.005f * Speed;
            if (newX >= UpperBoundary)
            {
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }
            else
            {
                up = false;
            }
        }

        if (!up)
        {
            newX = transform.position.x + 0.005f * Speed;
            if (newX <= LowerBoundary)
            {
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }
            else
            {
                up = true;
            }
        }
    }

    private void MoveY()
    {
        float newY;
        if (up)
        {
            newY = transform.position.y + 0.005f * Speed;
            if (newY <= UpperBoundary)
            {
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
            else
            {
                up = false;
            }
        }

        if (!up)
        {
            newY = transform.position.y - 0.005f * Speed;
            if (newY >= LowerBoundary)
            {
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
            else
            {
                up = true;
            }
        }
    }
}
