using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayOptionUI : BaseUI
{
    private Player[] _players;

    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _prevButton;
    [SerializeField] private Button _selectButtons;

    [Header("Player Info")]
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _descriptionText;
    [SerializeField] private Image _characterImage;
    [SerializeField] private TextMeshProUGUI _selectedText;

    [Header("Mode Info")]
    [SerializeField] Button _stageSelectButton;
    [SerializeField] Button _infinityModeButton;

    [SerializeField] private Button _exitButton;

    private int _index = 0;
    private int _selectedIndex = 0;

    protected override UIState GetUIState()
    {
        return UIState.PlayOption;
    }

    public override void Init()
    {
        _players = GameManager.Instance.Players;
        for (int i = 1; i < _players.Length; i++)
        {
            _players[i].gameObject.SetActive(false);
        }

        _nextButton.onClick.RemoveAllListeners();
        _prevButton.onClick.RemoveAllListeners();
        _selectButtons.onClick.RemoveAllListeners();
        _stageSelectButton.onClick.RemoveAllListeners();
        _infinityModeButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();

        _nextButton.onClick.AddListener(() => ChangeCharacter(1));
        _prevButton.onClick.AddListener(() => ChangeCharacter(-1));
        _selectButtons.onClick.AddListener(() => PlayerSelect());
        _stageSelectButton.onClick.AddListener(() => UIManager.Instance.ShowStageSelectUI());
        _infinityModeButton.onClick.AddListener(() => GameManager.Instance.StartInfiniteMode());
        _exitButton.onClick.AddListener(() => UIManager.Instance.BackToPrevUI());

        ChangeCharacter(0);
    }

    private void Update()
    {
        _characterImage.sprite = _players[_index].GetComponentInChildren<SpriteRenderer>().sprite;
    }

    void ChangeCharacter(int i)
    {
        _index += i;
        if (_index < 0)
        {
            _index = 0;
        }
        else if (_index >= _players.Length)
        {
            _index = _players.Length - 1;
        }

        if (_index == _selectedIndex)
        {
            _selectedText.text = "Selected";
        }
        else
        {
            _selectedText.text = "Select";
        }

        _nameText.text = GameManager.Instance.Players[_index].CharacterName;
        _descriptionText.text = GameManager.Instance.Players[_index].CharacterDescription;
        _characterImage.preserveAspect = true;
    }

    void PlayerSelect()
    {
        for (int i = 0; i < _players.Length; i++)
        {
            _players[i].gameObject.SetActive(i == _index);
        }

        _selectedText.text = "Selected";
        _selectedIndex = _index;
        GameManager.Instance.Player = _players[_selectedIndex];
    }
}
