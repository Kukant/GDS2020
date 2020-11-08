using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public bool DEBUG = false;

    public GameObject Menu;
    public GameObject Intro;

    public List<GameObject> LevelPrefabs;
    public GameObject currentLevel;

    public bool Sounds = true;
        
    private MenuScript menuScript;
    private Dictionary<string, AudioSource> soundCollection = new Dictionary<string, AudioSource>();

    // Start is called before the first frame update
    void Start()
    {
        soundCollection = InitSounds();
        if (!DEBUG) {
            SoundController("menu", true);
            MenuStart();
        } else {
            SoundController("game", true);
            Menu.SetActive(false);
        }

        menuScript = Menu.GetComponent<MenuScript>();
    }
    
    private Dictionary<string, AudioSource> InitSounds() {
        var sounds = new Dictionary<string, AudioSource>();
        sounds.Add("type", GameObject.Find("TypeSound").GetComponent<AudioSource>());
        sounds.Add("menu", GameObject.Find("MenuMusic").GetComponent<AudioSource>());
        sounds.Add("game", GameObject.Find("GameMusic").GetComponent<AudioSource>());
        return sounds;
    }
    
    public void SoundController(string key, bool play) {
        AudioSource s;
        if (soundCollection.TryGetValue(key, out s))
            if (play && (key == "game" || key == "menu" || Sounds)) s.Play();
            else s.Stop();
    }

    // Update is called once per frame
    public void RunLevel(int idx) {
        SoundController("menu", false);
        if (Sounds) {
            SoundController("type", true);
        }

        Intro.GetComponentInChildren<IntroScript>().LevelPrefab = LevelPrefabs[idx];
        Intro.SetActive(true);
        Menu.SetActive(false);
    }

    public void EndLevelSuccess(int remainingLives) {
        SoundController("game", false);
        if (Sounds) {
            SoundController("menu", true);
        }
        
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
        // gameObject.GetComponentInChildren<MenuScript>().ReloadScores();
    }
}

public class GameSaving {
    public int[] scores;
}
