using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI _curScoreText;
    [SerializeField] private TextMeshProUGUI _maxScoreText;
    [SerializeField] private Button _restartBtn;
    [SerializeField] private Button _homeBtn;

    public override void Init()
    {
        _restartBtn.onClick.RemoveAllListeners();
        _homeBtn.onClick.RemoveAllListeners();

        _restartBtn.onClick.AddListener(delegate
        {
            GameManager.Instance.Restart();
        });
        _homeBtn.onClick.AddListener(delegate
        {
            SceneManager.LoadScene("SampleScene");
        });
    }

    public override void OnChangedState()
    {
        _curScoreText.text = "Cur Score: " + GameManager.Instance.CurScore.ToString();
        _maxScoreText.text = "Max Score: " + GameManager.Instance.MaxScore.ToString();
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
}
