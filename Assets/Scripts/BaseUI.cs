using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Home,
    InGame,
    GameOver,
    Tutorial,
    Option,
    StageSelect,
    ModeSelect,
    PlayerSelect
}

public abstract class BaseUI : MonoBehaviour
{
    public virtual void Init()
    {

    }

    protected abstract UIState GetUIState();

    public void SetActive(UIState state)
    {
        gameObject.SetActive(GetUIState() == state);
    }
}
