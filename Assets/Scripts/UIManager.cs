using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour

    
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI restartText;
    // Start is called before the first frame update
    void Start()
    {
        if (restartText == null)
            Debug.LogError("restart text is null");

        if (scoreText == null)
            Debug.LogError("score text is null");

        restartText.gameObject.SetActive(false);
    }

    public void SetRestart()
    {
        restartText.gameObject.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
