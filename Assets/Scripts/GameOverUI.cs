using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : BaseUI
{
    public override void Init()
    {
        base.Init();
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            GameManager.Instance.StartGame();
        }
    }
}
