using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Player : MonoBehaviour
{
    Animator _animator;
    Rigidbody2D _rigidbody;
    CircleCollider2D _circleCollider;
    SpriteRenderer _sprite;

    [Header("Player State")] //인스팩터에서 조정 가능
    [SerializeField] protected float _maxHp;
    [SerializeField] protected float _hp;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _jumpForce;
    [SerializeField] protected float _invincibleCooldown;
    public string CharactorDescription;

    [Header("Player Default Value")] //초기화때 사용할 기본값
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
            if(value < 8f)
            {
                _jumpForce = 8f;
            }
            else
            {
                _jumpForce = value;
            }
        }
    }

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
        _sprite = GetComponentInChildren<SpriteRenderer>();

        _originalColliderSize = _circleCollider.radius;
    }

    public virtual void Init()
    {
        Hp = MaxHp;
        Speed = DefaultSpeed;
        JumpForce = _jumpForce;
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
            }
        }

        if (!_isStun)
        {
            if (_canJump && Input.GetKeyDown(KeyCode.Space))
            {
                _animator.SetTrigger("IsJump");
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
            velocity.y = JumpForce;
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

    private IEnumerator PlayerStun()
    {
        _isStun = true;
        _isInvincible = true;

        StartCoroutine(InvincibleAnimation());

        yield return new WaitForSeconds(3f);

        _isStun = false;

        yield return null;
    }

    private IEnumerator InvincibleAnimation()
    {
        while (_isInvincible)
        {
            SetSpriteAlpha(0.1f);
            yield return new WaitForSeconds(0.2f);

            SetSpriteAlpha(1f);
            yield return new WaitForSeconds(0.2f);
        }

        SetSpriteAlpha(1f);
    }

    private void SetSpriteAlpha(float alpha)
    {
        Color color = _sprite.color;
        color.a = alpha;
        _sprite.color = color;
    }

    public void Coin()
    {
        GameManager.Instance.UpdateScore(1);
    }

    public void ChangeSpeed(float value)
    {
        Speed += value;
    }

    public void ChangeHp(float value)
    {
        if (_isInvincible && value < 0) return;
        Hp += value;
        if (value < 0)
        {
            StartCoroutine(PlayerStun());
        }
    }

    public void EndPoint()
    {
        GameManager.Instance.GameOver();
    }

    private void OnTriggerStay2D(Collider2D collision) //플레이어 오브젝트 충돌 로직
    {
        if (!collision.CompareTag("Object")) return;

        Tilemap tilemap = collision.gameObject.GetComponent<Tilemap>();
        Object collisionObject = collision.GetComponent<Object>();

        bool isObstacle =
            (
            collisionObject.ObjectType == ObjectType.NormalObstacle ||
            collisionObject.ObjectType == ObjectType.Arrow ||
            collisionObject.ObjectType == ObjectType.EndPoint
            );

        if (tilemap != null && !isObstacle)
        {
            Vector3 hitPoint = collision.ClosestPoint(transform.position);
            Vector3Int cellPosition = tilemap.WorldToCell(hitPoint);
            if (tilemap.HasTile(cellPosition))
                tilemap.SetTile(cellPosition, null);
        }
        else if(!isObstacle)
        {
            collision.gameObject.SetActive(false);
        }
    }
}