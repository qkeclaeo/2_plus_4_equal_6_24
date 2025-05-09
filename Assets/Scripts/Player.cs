using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;
    CircleCollider2D _circleCollider;

    [Header("Player State")] //일단 다른데에서 쓸 수도 있으니 public으로 남겨둡니다.

    public float HP = 100;
    public float Speed = 3f;
    public float JumpForce = 8f;

    [SerializeField] private bool godMode = false;

    float deathCooldown = 0f;
    float invincibleCooldown = 0f;
    float originalColliderSize = 0f;
    

    bool isJump = false;
    bool canJump = true;
    bool isSlide = false;
    bool isSliding = false;
    bool isDead = false;

    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;

        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();

        if (animator == null)
            Debug.LogError($"{gameObject.name}의 애니메이터를 찾을 수 없습니다. ");

        if (_rigidbody == null)
            Debug.LogError($"{gameObject.name}의 리지드바디를 찾을 수 없습니다.");

        if (_circleCollider != null)
        {
            originalColliderSize = _circleCollider.radius;
        }
        else Debug.LogError($"{gameObject.name}의 서클콜라이더를 찾을 수 없습니다.");
    }

    void Update()
    {
        if (isDead)
        {
            if (deathCooldown <= 0)
            {
                //게임오버 로직
            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
        }
        else
        {
            invincibleCooldown -= Time.deltaTime;
            if (canJump && Input.GetKeyDown(KeyCode.Space))
                isJump = true;

            isSlide = Input.GetKey(KeyCode.LeftShift);
        }
    }



    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = Speed;

        if (isJump)
        {
            velocity.y += JumpForce;
            isJump = false;
            canJump = false;
        }

        if (isSlide)
        {
            if(!isSliding)
            {
                transform.position += Vector3.down * 0.25f;
                Debug.Log("위치변경! 아래로!");
            }
            isSliding = true;
            _circleCollider.radius = 0.25f;
        }
        else if (!isSlide)
        {
            if (isSliding)
            {
                transform.position += Vector3.up * 0.25f;
                Debug.Log("위치변경! 위로!");
            }
            isSliding = false;
            _circleCollider.radius = originalColliderSize;
        }

        _rigidbody.velocity = velocity;
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.tag == "BackGround") //바닥과 닿아야만 다시 점프 가능
        {
            canJump = true;
            Debug.Log("점프 가능!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //트리거 들어갔을때
    {
        if (collision.gameObject.name == "Opstacle") return;
        Tilemap tilemap = collision.gameObject.GetComponent<Tilemap>(); //타일맵 받아오기
        if (tilemap != null)
        {
            Vector3 hitPoint = collision.ClosestPoint(transform.position); //플레이어 위치와 가까운 위치 찾기
            Vector3Int cellPosition = tilemap.WorldToCell(hitPoint); //월드위치에서 셀위치 찾기
            if (tilemap.HasTile(cellPosition)) //해당 셀에 타일이 있다면
                tilemap.SetTile(cellPosition, null); //해당 타일 지우기
        }
    }
}
