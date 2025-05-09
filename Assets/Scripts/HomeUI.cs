using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeUI : BaseUI
{
    public override void Init()
    {
        Time.timeScale = 0f;
        base.Init();
    }

    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            //GameManager.Instance.StartGame();
        }
    }
}
