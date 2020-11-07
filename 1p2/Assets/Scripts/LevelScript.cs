using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour {

    private bool running = true;
    public string msg1, msg2, msg3, msg4;
    private string[] messagesArr;
    
    private int maxLives = 4;
    private int lives = 4;
    
    // Start is called before the first frame update
    void Start() {
        messagesArr = new[] {msg1, msg2, msg3, msg4};
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

    public void PlayerHit() {
        // set messagesArr[maxLives-lives]
    }

    public void Run() {
        // todo set messagesArr[0]
        running = true;
        lives = 4;
    }

    public void EndLevelSuccess() {
        running = false;
        // todo show some UI 
    }
}
