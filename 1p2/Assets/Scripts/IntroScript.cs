using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour {
    private GameObject currentLevel;
    private Text textBox;
    private string text;
    
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        
    }
    
    void OnEnable()
    {
        textBox = gameObject.GetComponent<Text>();
        textBox.rectTransform.localScale = new Vector3(1, 1, 1);
        textBox.text = "";

        currentLevel = GameObject.Find("GameController").GetComponent<GameController>().currentLevel;
        //string introText = GameObject.Find("GameController").GetComponent<GameController>().currentLevel.GetComponent<LevelText>().DisplayedText;
        text = "La puza palula pali pal liilil piali pullilipu lap lalap pal lal palpaluza lapsazula pa"; //introText;
        //gameObject.GetComponent<RectTransform>().pivot = GameObject.Find("Packet").transform.position;
        
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText() {
        for (int i = 0; i <= text.Length; ++i) {
            textBox.text = text.Substring(0, i);
            float timeNoise = Random.Range(0.002f, 0.01f);
            float sign = Random.Range(0, 1) > 0.5f ? 1 : -1;
            yield return new WaitForSeconds(0.06f + (timeNoise * sign));
        }
        yield return new WaitForSeconds(1);

        for (int i = 1; i < 35; ++i) {
            float newScale = 1 - ((1/35f) * i);
            textBox.rectTransform.localScale = new Vector3(newScale, newScale, newScale);
            yield return new WaitForSeconds(0.05f);
        }
        currentLevel.GetComponent<LevelScript>().Run();
        transform.parent.gameObject.SetActive(false);
    }

}
