using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Player : MonoBehaviour
{
    Animator _animator;
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
        if (value < 0) StartCoroutine(PlayerStun());
    }

    public void EndPoint()
    {
        Debug.Log("EndPoint");
        GameManager.Instance.GameOver();
    }

    private void OnTriggerStay2D(Collider2D collision) //트리거 들어갔을때
    {
        if (!collision.CompareTag("Object")) return;

        Tilemap tilemap = collision.gameObject.GetComponent<Tilemap>(); //타일맵 받아오기
        Object collisionObject = collision.GetComponent<Object>();

        bool isObstacle =
            (
            collisionObject.ObjectType == ObjectType.NormalObstacle ||
            collisionObject.ObjectType == ObjectType.Arrow ||
            collisionObject.ObjectType == ObjectType.EndPoint
            );

        if (tilemap != null && !isObstacle)
        {
            Vector3 hitPoint = collision.ClosestPoint(transform.position); //플레이어 위치와 가까운 위치 찾기
            Vector3Int cellPosition = tilemap.WorldToCell(hitPoint); //월드위치에서 셀위치 찾기
            if (tilemap.HasTile(cellPosition)) //해당 셀에 타일이 있다면
                tilemap.SetTile(cellPosition, null); //해당 타일 지우기
            Debug.Log($"{cellPosition}에 있는 {collision.gameObject.name}의 타일을 제거");
        }
        else
        {
            collision.gameObject.SetActive(false);
        }
    }

}