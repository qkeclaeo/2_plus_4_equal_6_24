using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;
    CircleCollider2D _circleCollider;

    [Header("Player State")] //일단 다른데에서 쓸 수도 있으니 public으로 남겨둡니다.

    public int HP = 100;
    public float Speed = 3f;
    public float JumpForce = 6f;

    [SerializeField] private bool godMode = false;

    float deathCooldown = 0f;
    float invincibleCooldown = 0f;
    Vector2 originalColliderOffset = Vector2.zero;
    float originalColliderSize = 0f;

    bool isJump = false;
    bool canJump = true;
    bool isSlide = false;
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
            originalColliderOffset = _circleCollider.offset;
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
            _circleCollider.offset = new Vector2(0f, -0.3f);
            _circleCollider.radius = 0.25f;
        }
        else if (!isSlide)
        {
            _circleCollider.offset = originalColliderOffset;
            _circleCollider.radius = originalColliderSize;
        }

        _rigidbody.velocity = velocity;

        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.tag == "BackGround") //바닥과 닿아야만 다시 점프 가능
        {
            canJump = true;
            Debug.Log("점프 가능!");
        }

        if (godMode || isDead) return; //갓모드거나 죽었다면 계산 안함

        if (collision.gameObject.tag == "Obstacle" && invincibleCooldown > 0) return; //장애물이랑 충돌했는데 무적시간이면 계산안함

        int value = 0;
        Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();

        if (obstacle != null)
        {
            //장애물의 값을 받아 플레이어 체력에 합산하는 부분
            value = 000;
            HP += value;
        }

        /* 아이템의 값을 받아 플레이어 체력에 합산하는 부분(아이템 추가 이후 작동하도록 주석화)
        Item item = collision.gameObject.GetComponent<Item>();
        if(item != null)
        {
            value = 000;
            HP += value;
        }
        */
    }
}
