using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_02 : Player
{
    [Header("Ability")]
    [SerializeField] private int _maxJump;
    [SerializeField] private int _extraJump;

    public override void Init()
    {
        base.Init();
        _extraJump = _maxJump;
    }

    protected override void Update()
    {
        if (!_canJump && _extraJump > 0)
        {
            _canJump = true;
            _extraJump--;
        }
        base.Update();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameManager.Instance.IsReadyToStart)
        {
            return;
        }

        if (collision.gameObject.CompareTag("BackGround"))
        {
            _canJump = true;
            _extraJump = _maxJump;
        }
    }
}
