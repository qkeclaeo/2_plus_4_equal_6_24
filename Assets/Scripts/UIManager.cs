using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour

    
{
    public static UIManager Instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI restartText;

    private void Awake()
    {
        if (Instance = null)
            Instance = this;
        else Destroy(gameObject);
    }

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

    public void GameOver()
    {
        //게임오버 UI표시
    }
}
