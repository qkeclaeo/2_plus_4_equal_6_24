using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : BaseUI
{
    [SerializeField] private Button _exitButton;

    public override void Init()
    {
        _exitButton.onClick.RemoveAllListeners();

        _exitButton.onClick.AddListener(OnClickExit);
    }

    protected override UIState GetUIState()
    {
        return UIState.Option;
    }

    private void OnClickExit()
    {
        UIManager.Instance.BackToPrevUI();
    }
}
