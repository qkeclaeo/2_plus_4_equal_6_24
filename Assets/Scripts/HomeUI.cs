using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _optionButton;
    [SerializeField] private Button _quitButton;

    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

    public override void Init()
    {
        _playButton.onClick.RemoveAllListeners();
        _optionButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();

        _playButton.onClick.AddListener(OnClickPlay);
        _optionButton.onClick.AddListener(OnClickOption);
        _quitButton.onClick.AddListener(OnClickQuit);
    }

    private void OnClickPlay()
    {
        // Todo: ĳ���� ����â �̵� ���� �ۼ�
    }

    private void OnClickOption()
    {
        // Todo: �ɼ� UI �̵� ���� �ۼ�
    }

    private void OnClickQuit()
    {
        Application.Quit();
    }
}
