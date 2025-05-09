using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : BaseUI
{
    [SerializeField] private Slider _hpBar;
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private Player _player;

    public override void Init()
    {
        //_player = GameManager.Instance.Player;
        base.Init();
    }

    protected override UIState GetUIState()
    {
        return UIState.InGame;
    }

    void Update()
    {
        // 체력바
        //float tempHp = _player.Hp / _player.MaxHp;
        //_hpText.text = (int)_player.Hp + " / " + (int)_player.MaxHp;
        //_hpBar.value = Mathf.Lerp(_hpBar.value, tempHp, Time.deltaTime * 10f);

        // 스코어
        //_scoreText.text = GameManager.Instance.CurScore.ToString();
    }
}
