using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] stage;
    [SerializeField] private Player player;

    GameObject curStage;
    static GameManager gameManager;
    public static GameManager Instance { get { return gameManager; } }

    int currentScore = 0;
    int currentStage = 1;        //재시작에 필요

    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float minSpeed = 3f;
    [SerializeField] private float speedIncreaseRate = 0.1f;
    [SerializeField] private float hpDecreaseRate = 1f;

    float currentSpeed = 3f;
    bool isOver = false;

    private void Awake()
    {
        gameManager = this;
        player = FindAnyObjectByType<Player>();
    }
    public void Start()
    {
        StageStart(currentStage);
    }

    public void StageStart(int stage_num)
    {
        if(curStage != null)
        {
            isOver = false;
            currentStage = stage_num;
            curStage.SetActive(false);
            curStage = stage[currentStage - 1];
            curStage.SetActive(true);
        }
        //UIManager.Instance.UpdateScore(0);
    }

    public void GameOver()
    {
        isOver = true;
        UIManager.Instance.GameOver();
    }

    public void Restart()
    {
        StageStart(currentStage);
    }

    public void UpdateScore(int score)
    {
        currentScore += score;
        //UIManager.Instance.UpdateScore(currentScore);
    }

    private void Update()
    {
        if (isOver) return;
        if(player != null)
        {
            ChangePlayerHP(-hpDecreaseRate * Time.deltaTime);
            if (player.HP <= 0)
            {
                GameOver();
            }
            if (player.Speed >= maxSpeed)
            {
                StartCoroutine(SlowDown());
                return;
            }
            else ChangePlayerSpeed(speedIncreaseRate * Time.deltaTime);
        }
        else
        {
            //예외처리
            Debug.LogError("player is Null");
        }
    }

    public void ChangePlayerHP(float value)
    {
        player.HP = Mathf.Clamp(player.HP + value,0,player.maxHP);
    }

    public void ChangePlayerSpeed(float value)
    {
        player.Speed = Mathf.Clamp(player.Speed+value,minSpeed,maxSpeed);
    }

    private IEnumerator SlowDown()
    {
        while (player.Speed > maxSpeed)
        {
            player.Speed -= Time.deltaTime * 5f;
            if (player.Speed < maxSpeed)
            {
                player.Speed = maxSpeed;
            }
            yield return null;
        }
    }
}
