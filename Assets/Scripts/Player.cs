using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Player : MonoBehaviour
{
    protected Animator _animator;

    Rigidbody2D _rigidbody;
    CircleCollider2D _circleCollider;

    [Header("Player State")]
    [SerializeField] protected float _maxHp;
    [SerializeField] protected float _hp;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _jumpForce;
    [SerializeField] protected float _invincibleCooldown;
    public string CharactorDescription;

    [Header("Player Default Value")]
    [SerializeField] protected float _defaultSpeed;
    [SerializeField] protected float _defaultInvincibleCooldown;

    public float MaxHp
    {
        get
        {
            return _maxHp;
        }
        set
        {
            if (value < 1f)
            {
                _maxHp = 1f;
            }
            else
            {
                _maxHp = value;
            }
        }
    }
    public float Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            if (value > _maxHp)
            {
                _hp = _maxHp;
            }
            else if (value < 0f)
            {
                _hp = 0f;
            }
            else
            {
                _hp = value;
            }
        }
    }
    public float DefaultSpeed
    {
        get
        {
            return _defaultSpeed;
        }
        set
        {
            if(value < 3f)
            {
                _defaultSpeed = 3f;
            }
            else
            {
                _defaultSpeed = value;
            }
        }
    }
    public float Speed
    {
        get => _speed;
        set
        {
            if(value < 3f)
            {
                _speed = 3f;
            }
            else
            {
                _speed = value;
            }
        }
    }
    public float JumpForce
    {
        get => _jumpForce;
        set
        {
            if(value < 8)
            {
                _jumpForce = 8f;
            }
            else
            {
                _jumpForce = value;
            }
        }
    }

    [SerializeField] protected bool _godMode = false;


    protected float _originalColliderSize;

    protected bool _isJump = false;
    protected bool _canJump = true;
    protected bool _isSlideInput = false;
    protected bool _isSliding = false;

    protected bool _isInvincible = false;
    protected bool _isStun = false;

    void OnEnable()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();

        // 인스펙터에서 수정
        //MaxHp = 100f;
        //Hp = 100f;
        //Speed = 3f;
        //JumpForce = 8f;

        _originalColliderSize = _circleCollider.radius;
    }

    public virtual void Init()
    {
        Hp = MaxHp;
        Speed = DefaultSpeed;
        _invincibleCooldown = _defaultInvincibleCooldown;
        _isStun = false;
        transform.position = Vector3.up * 7.5f;
    }

    protected virtual void Update()
    {
        if(_isInvincible)
        {
            _invincibleCooldown -= Time.deltaTime;
            if(_invincibleCooldown <= 0)
            {
                _invincibleCooldown = _defaultInvincibleCooldown;
                _isInvincible = false;
                _animator.SetBool("IsInvincible", false);
            }
        }

        if (!_isStun)
        {
            if (_canJump && Input.GetKeyDown(KeyCode.Space))
            {
                _isJump = true;
            }

            _isSlideInput = Input.GetKey(KeyCode.LeftShift);
        }
    }

    private void FixedUpdate()
    {
        Vector3 velocity = _rigidbody.velocity;
        switch (_isStun)
        {
            case false:
                velocity.x = Speed;
                break;

            case true:
                velocity.x = 0f;
                break;
        }

        if (_isJump)
        {
            velocity.y += JumpForce;
            _isJump = false;
            _canJump = false;
        }

        _rigidbody.velocity = velocity;

        switch (_isSlideInput)
        {
            case true:
                if (!_isSliding)
                {
                    transform.position += Vector3.down * (_originalColliderSize / 2);
                    _circleCollider.radius = (_originalColliderSize / 2);
                }
                _isSliding = true;
                break;
            case false:
                if (_isSliding)
                {
                    transform.position += Vector3.up * (_originalColliderSize / 2);
                    _circleCollider.radius = _originalColliderSize;
                }
                _isSliding = false;
                break;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BackGround"))
        {
            _canJump = true;
        }
    }

    private IEnumerator PlayerStun() //장애물과 충돌 시 작동시킬 코루틴
    {
        _isStun = true;
        _isInvincible = true;

        yield return new WaitForSeconds(3f);

        _isStun = false;

        yield return null;
    }

    public void Coin()
    {
        Debug.Log("Coin");
        GameManager.Instance.UpdateScore(1);
    }

    public void ChangeSpeed(float value)
    {
        Debug.Log($"{(value > 0 ? "SpeedUp" : "SpeedDown")}");
        Speed += value;
    }

    public void ChangeHp(float value)
    {
        if (_isInvincible && value < 0) return;
        Debug.Log($"{(value > 0 ? "Heal" : "Damage")}");
        Hp += value;
        if (value < 0)
        {
            StartCoroutine(PlayerStun());
            _animator.SetBool("IsInvincible", true);
        }
    }

    public void EndPoint()
    {
        Debug.Log("EndPoint");
        GameManager.Instance.GameOver();
    }
}