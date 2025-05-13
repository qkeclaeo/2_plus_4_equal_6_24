using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : BaseUI
{
    [SerializeField] private Sprite[] _images;
    [SerializeField] private Image _displayImage;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _prevButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private TextMeshProUGUI _indexText;

    private int _imageIndex = 0;

    public override void Init()
    {
        _displayImage = GetComponentInChildren<Image>();
        _indexText = GetComponentInChildren<TextMeshProUGUI>();

        _nextButton.onClick.RemoveAllListeners();
        _prevButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();

        _nextButton.onClick.AddListener(OnClickNext);
        _prevButton.onClick.AddListener(OnClickPrev);
        _exitButton.onClick.AddListener(OnClickExit);

        UpdateUI();
    }

    protected override UIState GetUIState()
    {
        return UIState.Tutorial;
    }

    private void UpdateUI()
    {
        _displayImage.sprite = _images[_imageIndex];
        _indexText.text = $"{_imageIndex + 1} / {_images.Length}";
    }

    private void OnClickNext()
    {
        Debug.Log("다음");
        if (_imageIndex < _images.Length - 1)
        {
            _imageIndex++;
        }

        UpdateUI();
    }

    private void OnClickPrev()
    {
        Debug.Log("이전");
        if (_imageIndex > 0)
        {
            _imageIndex--;
        }

        UpdateUI();
    }

    private void OnClickExit()
    {
        // 튜토리얼 UI 종료
        Debug.Log("튜토리얼 종료");
    }
}
