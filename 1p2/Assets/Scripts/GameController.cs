using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour {
    public bool DEBUG = false;

    public GameObject Menu;

    public List<GameObject> LevelPrefabs;
    private GameObject currentLevel;

    private MenuScript menuScript;

    // Start is called before the first frame update
    void Start()
    {
        Save(10, 1);
        if (!DEBUG) {
            MenuStart();
        } else {
            Menu.SetActive(false);
        }

        menuScript = Menu.GetComponent<MenuScript>();
    }

    // Update is called once per frame
    public void RunLevel(int idx) {
        currentLevel = Instantiate(LevelPrefabs[idx]);
        currentLevel.GetComponent<LevelScript>().Setup();
        currentLevel.GetComponent<LevelScript>().Run();
        Menu.SetActive(false);
    }

    public void EndLevelSuccess(int remainingLives) {
        Destroy(currentLevel);
        MenuStart();
    }

    public void MenuStart() {
        Menu.SetActive(true);
    }
    
    public void Save(int score, int level) {
        GameSaving data;
        if (File.Exists(Application.persistentDataPath + "/saves.json"))
            data = JsonUtility.FromJson<GameSaving>(File.ReadAllText(Application.persistentDataPath + "/saves.json"));
        else {
            data = new GameSaving();
            data.scores = new []{ 0, 0, 0, 0 };
        }
        data.scores[level - 1] = score;
        string jsonData = JsonUtility.ToJson (data, true);
        File.WriteAllText (Application.persistentDataPath + "/saves.json", jsonData);
        gameObject.GetComponentInChildren<MenuScript>().ReloadScores();
    }
}

public class GameSaving {
    public int[] scores;
}
