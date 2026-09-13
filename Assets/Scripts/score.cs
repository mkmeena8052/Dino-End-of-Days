using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TextController : MonoBehaviour
{
    private TMP_Text text;
    private float myTime = 0f;
    public int score = 0;
    void Awake()
    {
        text = GetComponent<TMP_Text>();
        myTime = 0f;
    }

    void Update()
    {
        myTime += Time.deltaTime;

        score = Mathf.RoundToInt(myTime) * 100;
        UpdateText();
    }
    void UpdateText()
    {
        text.text = "Score: " + score;
    }
}

