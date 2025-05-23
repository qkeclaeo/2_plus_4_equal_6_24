using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private UIState _prevState;
    private UIState _curState;
    [SerializeField] private BaseUI[] _uis;

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

    void Start()
    {
        foreach (var ui in _uis)
        {
            ui.Init();
        }

        ChangeState(UIState.Home);
    }

    private void ChangeState(UIState state)
    {
        _prevState = _curState;
        _curState = state;
        foreach (var ui in _uis)
        {
            ui.SetActive(state);
            ui.OnChangedState();
        }
    }

    public void BackToPrevUI()
    {
        switch (_curState)
        {
            case UIState.StageSelect:
                ChangeState(UIState.PlayOption);
                return;
            default:
                break;
        }
        ChangeState(UIState.Home);
    }

    public void StartGame()
    {
        ChangeState(UIState.InGame);
    }

    public void GameOver()
    {
        ChangeState(UIState.GameOver);
    }

    public void ShowTutorial()
    {
        ChangeState(UIState.Tutorial);
    }

    public void ShowOption()
    {
        ChangeState(UIState.Option);
    }

    public void ShowPlayOption()
    {
        ChangeState(UIState.PlayOption);
    }

    public void ShowStageSelectUI()
    {
        ChangeState(UIState.StageSelect);
    }

    public void ShowHomeUI()
    {
        ChangeState(UIState.Home);
    }
}
