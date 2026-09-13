using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TextController : MonoBehaviour
{
    private TMP_Text text;
    private float time = 0f;
    public int score;
    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        time += Time.deltaTime;

        score = Mathf.RoundToInt(time) * 100;
        UpdateText();
    }
    void UpdateText()
    {
        text.text = "Score: " + score;
    }
}

