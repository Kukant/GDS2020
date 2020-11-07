using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var otherRB = other.GetComponent<Rigidbody2D>();
        if (otherRB) {
            otherRB.gravityScale = 0f;
            var ls = GetComponentInParent<LevelScript>();
            ls.EndLevelSuccess();
        }
    }
}
