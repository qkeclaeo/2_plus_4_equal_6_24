using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] stage;
    [SerializeField] private GameObject player;

    GameObject curStage;
    static GameManager gameManager;
    public static GameManager Instance { get { return gameManager; } }

    private int currentScore = 0;
    int currentStage = 1;        //재시작에 필요

    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float speedIncreaseRate = 0.1f;
    [SerializeField] private float hpDecreaseRate = 1f;


    bool isOver = false;

    private void Awake()
    {
        gameManager = this;
    }
    public void Start()
    {
        StageStart(currentStage);
    }

    public void StageStart(int stage_num)
    {
        currentStage = stage_num;
        curStage.SetActive(false);
        curStage = stage[currentStage-1];
        curStage.SetActive(true);
        UIManager.Instance.UpdateScore(0);
    }

    public void GameOver()
    {
        UIManager.Instance.GameOver();
    }

    public void Restart()
    {
        StageStart(currentStage);
    }

    public void UpdateScore(int score)
    {
        currentScore += score;
        UIManager.Instance.UpdateScore(currentScore);
    }

    private void Update()
    {
        if (isOver) return;
        if(player != null)
        {
            player.DecreaseHp(decreaseHpRate * Time.deltaTime);
            if (player.CurrentHp <= 0)
            {
                isOver = true;
                GameOver();
            }
        }
        else
        {
            //예외처리
            Debug.LogError("player is Null");
        }
        currentSpeed = Mathf.Min(maxSpeed, currentSpeed + speedIncreaseRate * Time.deltaTime);
        player.SetSpeed(currentSpeed);
    }
}
