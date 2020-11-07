using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketDamage : MonoBehaviour {

    private bool immune = false;

    private SpriteRenderer sprite;

    private Color initialColor;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        initialColor = sprite.color;
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Colission with " + other.gameObject.name);
        if (!immune) {
            var dmg = other.gameObject.GetComponent<Damage>();
            if (dmg != null) {
                StopAllCoroutines();
                StartCoroutine("DamageBlinking");
                immune = true;
            }
        }
    }
    
    IEnumerator DamageBlinking()
    {

        for (var i = 0; i < 8; i++) {
            Debug.Log("changing color");
            if (i % 2 == 0) {
                sprite.color = Color.red;
            }
            else {
                sprite.color = initialColor;
            }
            yield return new WaitForSeconds(0.2f);
        }

        immune = false;
    }
}
