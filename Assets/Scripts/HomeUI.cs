using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _tutorialButton;
    [SerializeField] private Button _optionButton;
    [SerializeField] private Button _quitButton;

    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

    public override void Init()
    {
        _playButton.onClick.RemoveAllListeners();
        _tutorialButton.onClick.RemoveAllListeners();
        _optionButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();

        _playButton.onClick.AddListener(OnClickPlay);
        _tutorialButton.onClick.AddListener(OnClickTutorial);
        _optionButton.onClick.AddListener(OnClickOption);
        _quitButton.onClick.AddListener(OnClickQuit);
    }

    private void OnClickPlay()
    {
        // Todo: 캐릭터 선택창 이동 로직 작성
        GameManager.Instance.StartGame();
    }

    private void OnClickTutorial()
    {
        UIManager.Instance.ShowTutorial();
    }

    private void OnClickOption()
    {
        UIManager.Instance.ShowOption();
    }

    private void OnClickQuit()
    {
        Application.Quit();
    }
}
