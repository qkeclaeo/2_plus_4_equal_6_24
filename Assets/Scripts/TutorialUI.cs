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
        _displayImage = transform.Find("DisplayImage").GetComponent<Image>();
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

        if (_imageIndex == 0)
        {
            _prevButton.gameObject.SetActive(false);
            _nextButton.gameObject.SetActive(true);
        }
        else if (_imageIndex == _images.Length - 1)
        {
            _prevButton.gameObject.SetActive(true);
            _nextButton.gameObject.SetActive(false);
        }
        else
        {
            _prevButton.gameObject.SetActive(true);
            _nextButton.gameObject.SetActive(true);
        }
    }

    private void OnClickNext()
    {
        if (_imageIndex < _images.Length - 1)
        {
            Debug.Log("다음");
            _imageIndex++;

            UpdateUI();
        }
    }

    private void OnClickPrev()
    {
        if (_imageIndex > 0)
        {
            Debug.Log("이전");
            _imageIndex--;

            UpdateUI();
        }
    }

    private void OnClickExit()
    {
        _imageIndex = 0;
        UpdateUI();
        
        UIManager.Instance.BackToPrevUI();
    }
}
