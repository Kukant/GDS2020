using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform PortalOut;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Packet")
        {
            collision.gameObject.transform.position = PortalOut.position + new Vector3(2, 0, 0);
        }
    }
}
