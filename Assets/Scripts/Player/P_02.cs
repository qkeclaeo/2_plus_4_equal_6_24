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
        if (_isInvincible)
        {
            _invincibleCooldown -= Time.deltaTime;
            if (_invincibleCooldown <= 0)
            {
                _invincibleCooldown = _defaultInvincibleCooldown;
                _isInvincible = false;
            }
        }

        if (!_isStun)
        {
            if(!_canJump && _extraJump > 0)
            {
                _canJump = true;
                _extraJump--;
            }

            if (_canJump && Input.GetKeyDown(KeyCode.Space))
            {
                _isJump = true;
            }

            _isSlideInput = Input.GetKey(KeyCode.LeftShift);
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BackGround"))
        {
            _canJump = true;
            _extraJump = _maxJump;
        }
    }
}
