using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : BaseUI
{
    [SerializeField] private Sprite[] images;
    [SerializeField] private Image displayImage;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private TextMeshProUGUI indexText;
    private int imageIndex = 0;

    private void Start()
    {
        displayImage = GetComponentInChildren<Image>();
        indexText = GetComponentInChildren<TextMeshProUGUI>();

        Init();
    }

    public override void Init()
    {
        nextButton.onClick.AddListener(NextButton);
        prevButton.onClick.AddListener(PrevButton);
        exitButton.onClick.AddListener(ExitButton);

        UpdateUI();
    }

    protected override UIState GetUIState()
    {
        return UIState.Home;  // Tutorial로 변경
    }

    private void UpdateUI()
    {
        displayImage.sprite = images[imageIndex];
        indexText.text = $"{imageIndex + 1} / {images.Length}";
    }

    private void NextButton()
    {
        Debug.Log("다음");
        if (imageIndex < images.Length - 1) imageIndex++;
        UpdateUI();
    }

    private void PrevButton()
    {
        Debug.Log("이전");
        if (imageIndex > 0) imageIndex--;
        UpdateUI();
    }

    private void ExitButton()
    {
        // 튜토리얼 UI 종료
        Debug.Log("튜토리얼 종료");
    }
}
