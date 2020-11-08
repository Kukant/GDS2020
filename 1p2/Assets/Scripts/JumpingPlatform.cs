using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour {
    public float force = 1000f;

    private void OnTriggerStay2D(Collider2D other) {
        var rb = other.GetComponent<Rigidbody2D>();
        if (rb) {
            rb.AddForce(transform.up * force);
        }

    }
}
