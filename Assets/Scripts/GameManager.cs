using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject[] _stages;
    [SerializeField] private Player _player;

    public Player Player => _player;

    private GameObject _curStage;

    public int CurScore { get; private set; }

    /// <summary>
    /// 인덱스 X. 스테이지 번호 O (1부터 시작)
    /// </summary>
    private int _curStageNum = 1;

    [SerializeField] private float _maxSpeed = 10f;
    [SerializeField] private float _speedIncreaseRate = 0.1f;
    [SerializeField] private float _hpDecreaseRate = 1f;

    private bool _isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _player = FindAnyObjectByType<Player>();
    }
    public void Start()
    {
        Time.timeScale = 0f;
        //StageStart(_curStageNum);
    }

    public void StartGame()
    {
        CurScore = 0;
        Time.timeScale = 1f;
        StageStart(_curStageNum);
        UIManager.Instance.StartGame();
    }

    private void StageStart(int stageNum)
    {
        if (_curStage != null)
        {
            _isGameOver = false;
            _curStageNum = stageNum;
            _curStage.SetActive(false);
            _curStage = _stages[_curStageNum - 1];
            _curStage.SetActive(true);
        }

        CurScore = 0;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        _isGameOver = true;
        UIManager.Instance.GameOver();
    }

    public void Restart()
    {
        _curStageNum = 0;
        StartGame();
    }

    /// <summary>
    /// 스코어 변경 X. 스코어 증가 O
    /// </summary>
    /// <param name="score"></param>
    public void UpdateScore(int score)
    {
        CurScore += score;
    }

    private void Update()
    {
        if (_isGameOver)
        {
            return;
        }

        if (Player == null)
        {
            Debug.LogError("player is null.");
            return;
        }

        Player.Hp -= _hpDecreaseRate * Time.deltaTime;
        if (Player.Hp <= 0f)
        {
            GameOver();
        }

        if (Player.Speed >= _maxSpeed)
        {
            StartCoroutine(SlowDown());
            return;
        }
        else
        {
            Player.Speed += _speedIncreaseRate * Time.deltaTime;
        }
    }

    private IEnumerator SlowDown()
    {
        while (Player.Speed > _maxSpeed)
        {
            Player.Speed -= Time.deltaTime * 5f;
            if (Player.Speed < _maxSpeed)
            {
                Player.Speed = _maxSpeed;
            }

            yield return null;
        }
    }
}