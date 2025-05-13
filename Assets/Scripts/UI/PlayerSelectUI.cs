using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectUI : BaseUI
{
    private GameObject[] _playerObjects;
    private Player[] _players;

    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;
    [SerializeField] private Button _selectButtons;

    [Header("Player Info")]
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _descriptionText;
    [SerializeField] private Image _characterImage;
    [SerializeField] private TextMeshProUGUI _selectedText;

    private int _index = 0;
    private int _selectedIndex = 0;

    protected override UIState GetUIState()
    {
        return UIState.PlayerSelect;
    }

    public override void Init()
    {
        _players = FindObjectsOfType<Player>();
        _playerObjects = new GameObject[_players.Length];
        for (int i = 0; i < _players.Length; i++)
        {
            _playerObjects[i] = _players[i].gameObject;
            if (i != 0)
            {
                _playerObjects[i].SetActive(false);
            }
        }

        _nextButton.onClick.AddListener(() => ChangeCharacter(1));
        _previousButton.onClick.AddListener(() => ChangeCharacter(-1));
        _selectButtons.onClick.AddListener(() => PlayerSelect());

        ChangeCharacter(0);
    }

    private void Update()
    {
        _characterImage.sprite = _playerObjects[_index].GetComponentInChildren<SpriteRenderer>().sprite;
    }

    void ChangeCharacter(int i)
    {
        _index += i;
        if (_index < 0) _index = 0;
        else if (_index >= _playerObjects.Length) _index = (_playerObjects.Length - 1);

        if (_index == _selectedIndex)
        {
            _selectedText.text = "Selected";
        }
        else
        {
            _selectedText.text = "Select";
        }

        _nameText.text = _players[_index].CharacterName;
        _descriptionText.text = _players[_index].CharacterDescription;
        _characterImage.preserveAspect = true;
    }

    void PlayerSelect()
    {
        for (int i = 0; i < _playerObjects.Length; i++)
        {
            _playerObjects[i].SetActive(i == _index);
            _selectedText.text = "Selected";
            _selectedIndex = _index;
        }
    }
}
