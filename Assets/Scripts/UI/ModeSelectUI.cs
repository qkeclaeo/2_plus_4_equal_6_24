using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeSelectUI : BaseUI
{
    [SerializeField] Button stageSelectButton;
    [SerializeField] Button infinityModeButton;

    protected override UIState GetUIState()
    {
        return UIState.ModeSelect;
    }

    public override void Init()
    {
        stageSelectButton.onClick.RemoveAllListeners();
        infinityModeButton.onClick.RemoveAllListeners();

        stageSelectButton.onClick.AddListener(() => LoadStageSelectScene());
        infinityModeButton.onClick.AddListener(() => LoadInfinityModeScene());
    }

    void LoadStageSelectScene()
    {
        Debug.LogError("아직 스테이지 선택 버튼에 불러올 씬이 설정이 안되어 있습니다.");
        //SceneManager.LoadScene("AAAAAAAA");
    }

    void LoadInfinityModeScene()
    {
        Debug.LogError("아직 무한모드 버튼에 불러올 씬이 설정이 안되어 있습니다.");
        //SceneManager.LoadScene("BBBBBBBB");
    }
}
