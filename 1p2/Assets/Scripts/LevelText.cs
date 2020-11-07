using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    public string DisplayedText = string.Empty;
    private Text levelText;

    // Start is called before the first frame update
    void Start()
    {
        levelText = gameObject.GetComponent<Text>();
        levelText.text = DisplayedText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDisplayedText(string newText)
    {
        levelText.text = newText;
    }
}
