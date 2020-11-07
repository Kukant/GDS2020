using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour {

    private bool running = true;
    public string msg1, msg2, msg3, msg4;
    private string[] messagesArr;
    
    private int maxLives = 4;
    private int lives;
    private LevelText levelText;
    private GameObject packet;
    private Vector3 packetInitPos;
    
    // Start is called before the first frame update
    void Start() {
        messagesArr = new[] {msg1, msg2, msg3, msg4};
        if (GameObject.Find("Level Text Screen")) {
            levelText = GameObject.Find("Level Text Screen").GetComponentInChildren<LevelText>();
        }
        packet = GameObject.Find("Packet");
        Debug.Log(packet);
        packetInitPos = packet.transform.position;
        // todo transfer to UI shit
        Run();
    }

    // Update is called once per frame
    void Update()
    {
        if (running && Time.timeScale < 1) {
            Time.timeScale += 0.005f;
        }

        if (!running && Time.timeScale > 0) {
            if (Time.timeScale - 0.005f < 0) {
                Time.timeScale = 0f;
            } else {
                Time.timeScale -= 0.005f;
            }
        }
    }

    public void Restart() {
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
        levelText.ChangeDisplayedText(messagesArr[maxLives-lives]);
    }

    public void Run() {
        running = true;
        lives = 4;
        updateText();
    }

    public void EndLevelSuccess() {
        running = false;
        // todo show some UI 
    }
}
