using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour {
    public GameObject LevelPrefab;
    
    private GameController gameController;

    private Text textBox;
    private string text;
    
    // Start is called before the first frame update
    void Start() {
        gameController = GetComponentInParent<GameController>();
    }

    void OnEnable()
    {
        textBox = gameObject.GetComponent<Text>();
        textBox.rectTransform.localScale = new Vector3(1, 1, 1);
        textBox.text = "";
        
        text = LevelPrefab.GetComponent<LevelScript>().msg1;
        
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText() {
        for (int i = 0; i <= text.Length; ++i) {
            textBox.text = text.Substring(0, i);
            float timeNoise = Random.Range(0.002f, 0.01f);
            float sign = Random.Range(0, 1) > 0.5f ? 1 : -1;
            yield return new WaitForSeconds(0.06f + (timeNoise * sign));
        }
        gameController.SoundController("type", false);
        yield return new WaitForSeconds(1);

        for (int i = 2; i < 35; ++i) {
            float newScale = 1 - ((1/35f) * i);
            textBox.rectTransform.localScale = new Vector3(newScale, newScale, newScale);
            yield return new WaitForSeconds(0.05f);
        }
        if (gameController.Sounds) {
            gameController.SoundController("game", true);
        }

        gameController.InstantiateLevel();
        transform.parent.gameObject.SetActive(false);
    }

}
