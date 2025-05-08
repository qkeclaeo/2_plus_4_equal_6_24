using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] stage;
    GameObject curStage;
    static GameManager gameManager;
    public static GameManager Instance { get { return gameManager; } }

    private int currentScore = 0;

    UIManager uiManager;
    public UIManager UIManager { get { return uiManager; } }


    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<UIManager>();
    }
    public void Start()
    {
        uiManager.UpdateScore(0);
    }

    public void StageStart(int stage_num)
    {
        curStage.SetActive(false);
        curStage = stage[stage_num - 1];
        curStage.SetActive(true);
        uiManager.UpdateScore(0);
    }

    public void GameOver()
    {
        uiManager.SetRestart();
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateScore(int score)
    {
        currentScore += score;
        uiManager.UpdateScore(currentScore);
    }
}
