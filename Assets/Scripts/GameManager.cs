using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject[] _stages;
    [SerializeField] private Player[] _players;

    public Player[] Players => _players;
    public Player Player { get; set; }

    private GameObject _curStage;

    public int CurScore { get; private set; }
    public int MaxScore
    {
        get
        {
            if (PlayerPrefs.HasKey("MaxScore"))
            {
                return PlayerPrefs.GetInt("MaxScore");
            }
            else
            {
                return -1;
            }
        }
        private set
        {
            PlayerPrefs.SetInt("MaxScore", value);
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// 인덱스 X. 스테이지 번호 O (1부터 시작)
    /// </summary>
    private int _curStageNum = 1;

    [SerializeField] private float _maxSpeed = 10f;
    [SerializeField] private float _speedIncreaseRate = 0.1f;
    [SerializeField] private float _hpDecreaseRate = 1f;

    public bool IsReadyToStart { get; set; }
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
    }
    private void Start()
    {
        //_curStage = Instantiate(_stages[_curStageNum - 1]);
        //StageStart(_curStageNum);
    }

    private void StartGame()
    {
        if (Player == null)
        {
            Player = Players[0];
        }

        CurScore = 0;
        _isGameOver = false;
        Player.Init();
        Camera.main.GetComponent<FollowCamera>().Init(Player.transform);

        IsReadyToStart = true;
        UIManager.Instance.StartGame();
    }

    public void StartInfiniteMode()
    {
        SpawnManager.Instance.SetInfiniteMode(true);
        StartGame();
    }

    public void StageStart(int stageNum)
    {
        SpawnManager.Instance.SetInfiniteMode(false);
        if (_curStage != null)
        {
            Destroy(_curStage);
        }

        _curStageNum = stageNum;
        _curStage = Instantiate(_stages[_curStageNum - 1]);

        StartGame();
    }

    public void GameOver()
    {
        IsReadyToStart = false;
        _isGameOver = true;
        if (MaxScore < CurScore)
        {
            MaxScore = CurScore;
        }

        UIManager.Instance.GameOver();
    }

    public void Restart()
    {
        if (SpawnManager.Instance.IsInfinite)
        {
            StartInfiniteMode();
        }
        else
        {
            StageStart(_curStageNum);
        }

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
        if (!IsReadyToStart || _isGameOver)
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