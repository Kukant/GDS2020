using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour {
    private float force = 1000f;

    private void OnTriggerStay2D(Collider2D other) {
        var rb = other.GetComponent<Rigidbody2D>();
        if (rb) {
            rb.AddForce(Vector2.up * force);
        }

    }
}
