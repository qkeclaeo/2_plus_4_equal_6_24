using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayer : MonoBehaviour
{
    GameObject[] playerObjects;
    Player[] players;

    [SerializeField] Button nextButton;
    [SerializeField] Button previousButton;
    [SerializeField] Button selectButtons;

    [Header("Player Info")]
    [SerializeField] Text nameText;
    [SerializeField] Text descriptionText;
    [SerializeField] Image characterImage;
    [SerializeField] TextMeshProUGUI selectedText;

    int index = 0;
    int selectedIndex = 0;

    private void OnEnable()
    {
        players = FindObjectsOfType<Player>();
        playerObjects = new GameObject[players.Length];
        for (int i = 0; i < players.Length; i++)
        {
            playerObjects[i] = players[i].gameObject;
            if (i != 0) playerObjects[i].SetActive(false);
        }

        nextButton.onClick.AddListener(() => ChangeCharacter(1));
        previousButton.onClick.AddListener(() => ChangeCharacter(-1));
        selectButtons.onClick.AddListener(() => PlayerSelect());

        ChangeCharacter(0);
    }

    private void Update()
    {
        characterImage.sprite = playerObjects[index].GetComponentInChildren<SpriteRenderer>().sprite;
    }

    void ChangeCharacter(int i)
    {
        index += i;
        if (index < 0) index = 0;
        else if (index >= playerObjects.Length) index = (playerObjects.Length - 1);

        if (index == selectedIndex)
        {
            selectedText.text = "Selected";
        }
        else
        {
            selectedText.text = "Select";
        }

        nameText.text = players[index].CharacterName;
        descriptionText.text = players[index].CharacterDescription;
        characterImage.preserveAspect = true;
    }

    void PlayerSelect()
    {
        for (int i = 0; i < playerObjects.Length; i++)
        {
            playerObjects[i].SetActive(i == index);
            selectedText.text = "Selected";
            selectedIndex = index;
        }
    }
}
