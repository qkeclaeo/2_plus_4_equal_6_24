using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectUI : BaseUI
{
    [SerializeField] private Button[] _stageButtons;
    [SerializeField] private Button _exitButton;

    protected override UIState GetUIState()
    {
        return UIState.StageSelect;
    }

    public override void Init()
    {
        for (int i = 0; i < _stageButtons.Length; i++)
        {
            int stageNum = i + 1;
            _stageButtons[i].onClick.RemoveAllListeners();
            _stageButtons[i].onClick.AddListener(() => GameManager.Instance.StageStart(stageNum));
        }

        _exitButton.onClick.RemoveAllListeners();
        _exitButton.onClick.AddListener(() => UIManager.Instance.BackToPrevUI());
    }
}
