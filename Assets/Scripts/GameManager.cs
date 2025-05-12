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
        _curStage = Instantiate(_stages[_curStageNum - 1]);
        //StageStart(_curStageNum);
    }

    public void StartGame()
    {
        StageStart(_curStageNum);
    }

    private void StageStart(int stageNum)
    {
        CurScore = 0;
        Time.timeScale = 1f;
        if (_curStage != null)
        {
            if (_isGameOver)
            {
                _isGameOver = false;
            }
            Destroy(_curStage);
            Player.Init();
            _curStageNum = stageNum;
            _curStage = Instantiate(_stages[_curStageNum - 1]);
        }

        UIManager.Instance.StartGame();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        _isGameOver = true;
        if (MaxScore < CurScore)
        {
            MaxScore = CurScore;
        }

        UIManager.Instance.GameOver();
    }

    public void Restart()
    {
        StageStart(_curStageNum);
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