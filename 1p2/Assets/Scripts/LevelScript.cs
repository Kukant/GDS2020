﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour {
    public string msg1, msg2, msg3, msg4;
    private string[] messagesArr;
    
    private int maxLives = 4;
    public int lives;
    private LevelText levelText;
    private GameObject packet;
    private Vector3 packetInitPos;
    private GameController gc;
    private PacketMovement pm;
    
    // Start is called before the first frame update
    void Start() {
        Setup();
    }

    public void Setup() {
        messagesArr = new[] {msg1, msg2, msg3, msg4};
        levelText = GameObject.Find("GameScreen").GetComponentInChildren<LevelText>();
        packet = GameObject.Find("Packet");
        packetInitPos = packet.transform.position;
        pm = packet.GetComponent<PacketMovement>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame

    public void Restart() {
        packet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        packet.transform.position = packetInitPos;
        Run();
    }

    public void PlayerHit() {
        lives--;
        if (lives <= 0) {
            // TODO restart or go back to menu ??
            Restart();
        } else {
            updateText();
        }
    }

    private void updateText() {
        if (levelText)
            levelText.ChangeDisplayedText(messagesArr[maxLives-lives]);
    }

    public void Run() {
        lives = 4;
        updateText();
        StopAllCoroutines();
        StartCoroutine("SpeedUp");
    }

    public void EndLevelSuccess() {
        // todo show some UI 
        StopAllCoroutines();
        StartCoroutine("SlowDown");
    }

    IEnumerator SpeedUp() {
        pm.speed = 0;
        var steps = 60f;
        for (var i = 0; i < steps; i++) {
            pm.speed += pm.DefaultSpeed / steps;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator SlowDown()
    {
        packet.GetComponent<Rigidbody2D>().gravityScale = 0f;
        var steps = 60f;
        for (var i = 0; i < steps; i++) {
            pm.speed -= pm.DefaultSpeed / steps;
            
            if (i+1 == steps) {
                pm.speed = 0f;
                gc.EndLevelSuccess(lives);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
