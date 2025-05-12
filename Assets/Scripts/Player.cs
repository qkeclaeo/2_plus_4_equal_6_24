using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    Animator _animator;
    Rigidbody2D _rigidbody;
    CircleCollider2D _circleCollider;

    [Header("Player State")]
    [SerializeField] private float _maxHp;
    [SerializeField] private float _hp;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

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
        set => _jumpForce = value;
    }

    [SerializeField] private bool _godMode = false;

    private float _deathCooldown = 0f;
    [SerializeField] private float _invincibleCooldown = 0f;
    private float _originalColliderSize = 0f;


    private bool _isJump = false;
    private bool _canJump = true;
    private bool _isSlideInput = false;
    private bool _isSliding = false;

    public bool _isStun = false;

    void Start()
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

    public void Init()
    {
        _hp = _maxHp;
        _speed = 3f;
        _isStun = false;
        _invincibleCooldown = 0f;
        transform.position = Vector3.up * 7.5f;
    }

    void Update()
    {
        if(_invincibleCooldown > 0)
        {
            _invincibleCooldown -= Time.deltaTime;
        }
        else
        {
            _invincibleCooldown = 0f;
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

        if (_isSlideInput)
        {
            if (!_isSliding)
            {
                transform.position += Vector3.down * 0.25f;
            }

            _isSliding = true;
            _circleCollider.radius = 0.25f;
        }
        else
        {
            if (_isSliding)
            {
                transform.position += Vector3.up * 0.25f;
            }

            _isSliding = false;
            _circleCollider.radius = _originalColliderSize;
        }

        _rigidbody.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BackGround"))
        {
            _canJump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Object") return;
        ObjectType objectType = other.GetComponent<Object>().ObjectType;
        switch (objectType)
        {
            case ObjectType.Coin:
                {
                    Debug.Log("Coin");
                    GameManager.Instance.UpdateScore(1);
                }
                break;
            case ObjectType.SpeedUp:
                {
                    Debug.Log("SpeedUp");
                    Speed += 10f;
                }
                break;
            case ObjectType.SpeedDown:
                {
                    Debug.Log("SpeedDown");
                    Speed -= 10f;
                }
                break;
            case ObjectType.Heal:
                {
                    Debug.Log("Heal");
                    Hp += (MaxHp * 0.2f);
                }
                break;
            case ObjectType.NormalObstacle:
                {
                    Debug.Log("NrmalObstacle");
                    if(!_isStun && _invincibleCooldown == 0)
                    {
                        Hp -= 10f;
                        StartCoroutine(PlayerStun());
                    }
                }
                break;
            case ObjectType.Arrow:
                {
                    Debug.Log("ArrowObstacle");
                    if (!_isStun && _invincibleCooldown == 0)
                    {
                        Hp -= 10f;
                        StartCoroutine(PlayerStun());
                    }
                }
                break;
            case ObjectType.EndPoint:
                {
                    Debug.Log("EndPoint");
                    GameManager.Instance.GameOver();
                }
                break;
            default:
                {
                    Debug.Log("Item");
                }
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) //트리거 들어갔을때
    {
        if (collision.gameObject.tag != "Object") return;

        Object obj = collision.GetComponent<Object>();
        if (obj.ObjectType == ObjectType.NormalObstacle || obj.ObjectType == ObjectType.Arrow || obj.ObjectType == ObjectType.EndPoint) return;

        Tilemap tilemap = collision.gameObject.GetComponent<Tilemap>(); //타일맵 받아오기
        if (tilemap != null)
        {
            Vector3 hitPoint = collision.ClosestPoint(transform.position); //플레이어 위치와 가까운 위치 찾기
            Vector3Int cellPosition = tilemap.WorldToCell(hitPoint); //월드위치에서 셀위치 찾기
            if (tilemap.HasTile(cellPosition)) //해당 셀에 타일이 있다면
                tilemap.SetTile(cellPosition, null); //해당 타일 지우기
            Debug.Log($"{cellPosition}에 있는 {collision.gameObject.name}의 타일을 제거");
        }
    }

    private IEnumerator PlayerStun()
    {
        _isStun = true;
        _invincibleCooldown = 10f;

        yield return new WaitForSeconds(3f);

        _isStun = false;

        yield return null;
    }
}