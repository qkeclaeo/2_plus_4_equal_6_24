using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

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
        _curState = state;
        foreach (var ui in _uis)
        {
            ui.SetActive(state);
        }
    }

    public void StartGame()
    {
        ChangeState(UIState.InGame);
    }

    public void GameOver()
    {
        ChangeState(UIState.GameOver);
    }
}
