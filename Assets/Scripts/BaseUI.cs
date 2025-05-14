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
    PlayerSelect,
    PlayOption
}

public abstract class BaseUI : MonoBehaviour
{
    public virtual void Init()
    {

    }

    public virtual void OnChangedState()
    {

    }

    protected abstract UIState GetUIState();

    public void SetActive(UIState state)
    {
        gameObject.SetActive(GetUIState() == state);
    }
}
