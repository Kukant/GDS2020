using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
    public GameObject TitlePanel;
    public GameObject MenuPanel;
    public GameObject LevelsPanel;
    public GameObject FadingPanel;    
    public GameObject SoundsButton;
    public Text ScoreBox;
    
    public Sprite SoundOnIcon;
    public Sprite SoundOffIcon;

    public string level1Name;
    public GameObject Level1;
    public int level2Score;
    public string level2Name;
    public GameObject Level2;
    public int level3Score;
    public string level3Name;
    public GameObject Level3;
    public int level4Score;
    public string level4Name;
    public GameObject Level4;

    public float TitleDelaySeconds;
    public bool Sounds = true;
        
    private Color levelDone = new Color(0, 1, 0.043f);
    private Color levelUnlocked = new Color(1, 1, 1);
    private Color levelLocked = new Color(0.11f, 0.11f, 0.11f);
    
    private Image fadingImage;
    private Image soundsImage;
    private int state = 0;
    private int[] scores;
    private GameController gc;
    
    // Start is called before the first frame update
    void Start() {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        TitlePanel.SetActive(true);
        MenuPanel.SetActive(false);
        LevelsPanel.SetActive(false);
        FadingPanel.SetActive(false);
        
        ReloadScores();

        soundsImage = SoundsButton.GetComponent<Image>();
        soundsImage.sprite = SoundOnIcon;
        
        fadingImage = FadingPanel.GetComponent<Image>();
        fadingImage.color = new Color(0, 0, 0, 0);
        StartCoroutine(FadeTitle());
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("escape")) {
            if (state == 2) {
                ShowMenu();
            } else if (state == 1) {
                ExitGame();
            }
        }
    }

    IEnumerator FadeTitle() {
        while (state == 0) {
            fadingImage.color = new Color(0, 0, 0, 0);
            FadingPanel.SetActive(true);
            yield return new WaitForSeconds(TitleDelaySeconds);
            
            for (int i = 0; i < 100; i += 5) {
                fadingImage.color = new Color(0, 0, 0, i/100f);
                yield return new WaitForSeconds(0.04f);
            }
            ShowMenu();
            yield return 1;
        }
    }

    public void PlayLevel(int level) {
        state = 3;
        gc.RunLevel(level - 1);
    }

    private Color GetLevelColor(int score, int requiredScore, int levelScore) {
        if (score < requiredScore) {
            return levelLocked;
        }
        if (levelScore > 0) {
            return levelDone;
        }
        return levelUnlocked;
    }

    private string GetLevelText(string name, int requiredScore, int score) {
        return score >= requiredScore
            ? name + "\n------\n" + ConvertToBinary(score)
            : "LOCKED\nx\n" + ConvertToBinary(requiredScore);
    }

    public void ShowLevelSelect() {
        TitlePanel.SetActive(false);
        MenuPanel.SetActive(false);
        LevelsPanel.SetActive(true);
        FadingPanel.SetActive(false);

        int score = scores.Sum();
        
        Level1.GetComponent<Image>().color = GetLevelColor(score, 0, scores[0]);
        Level1.GetComponentInChildren<Text>().text = GetLevelText(level1Name, 0, scores[0]);
        Level1.GetComponent<Button>().onClick.AddListener(() => PlayLevel(1));
        
        Level2.GetComponent<Image>().color = GetLevelColor(score, level2Score, scores[1]);
        Level2.GetComponentInChildren<Text>().text = GetLevelText(level2Name, level2Score, scores[1]);
        if (score >= level2Score) {
            Level2.GetComponent<Button>().onClick.AddListener(() => PlayLevel(2));
        }

        Level3.GetComponent<Image>().color = GetLevelColor(score, level3Score, scores[2]);
        Level3.GetComponentInChildren<Text>().text = GetLevelText(level3Name, level3Score, scores[2]);
        if (score >= level3Score) {
            Level3.GetComponent<Button>().onClick.AddListener(() => PlayLevel(3));
        }

        Level4.gameObject.GetComponent<Image>().color = GetLevelColor(score, level4Score, scores[3]);
        Level4.GetComponentInChildren<Text>().text = GetLevelText(level4Name, level4Score, scores[3]);
        if (score >= level4Score) {
            Level4.GetComponent<Button>().onClick.AddListener(() => PlayLevel(4));
        }

        ScoreBox.text = "Score: " + ConvertToBinary(score);
        state = 2;
    }

    public void ShowMenu() {
        TitlePanel.SetActive(false);
        MenuPanel.SetActive(true);
        LevelsPanel.SetActive(false);
        FadingPanel.SetActive(false);
        
        state = 1;
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void ToggleSound() {
        Sounds = !Sounds;
        soundsImage.sprite = Sounds
            ? SoundOnIcon
            : SoundOffIcon;
    }
    
    public void ReloadScores() {
        scores = new[] {8, 0, 0, 0};
        GameSaving data;
        if (File.Exists(Application.persistentDataPath + "/saves.json"))
            data = JsonUtility.FromJson<GameSaving>(File.ReadAllText(Application.persistentDataPath + "/saves.json"));
        else {
            data = new GameSaving();
            data.scores = new []{ 0, 0, 0, 0 };
        }
        scores = data.scores;
    }

    private string ConvertToBinary(int number) {
        string binary = "";
        while (number > 0) {
            if (number % 2 == 0) {
                binary = "0" + binary;
            } else {
                binary = "1" + binary;
            }
            number = (int)Math.Floor(number / 2f);
        }

        for (int i = 8 - binary.Length; i > 0; --i) {
            binary = "0" + binary;
        }
        return binary.Substring(0, 4) + " " + binary.Substring(4);
    }
}
