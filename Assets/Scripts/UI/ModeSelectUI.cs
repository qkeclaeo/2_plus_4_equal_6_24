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
        Debug.LogError("���� �������� ���� ��ư�� �ҷ��� ���� ������ �ȵǾ� �ֽ��ϴ�.");
        //SceneManager.LoadScene("AAAAAAAA");
    }

    void LoadInfinityModeScene()
    {
        Debug.LogError("���� ���Ѹ�� ��ư�� �ҷ��� ���� ������ �ȵǾ� �ֽ��ϴ�.");
        //SceneManager.LoadScene("BBBBBBBB");
    }
}
