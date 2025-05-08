using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;
    CircleCollider2D _circleCollider;

    [Header("Player State")] //�ϴ� �ٸ������� �� ���� ������ public���� ���ܵӴϴ�.

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
            Debug.LogError($"{gameObject.name}�� �ִϸ����͸� ã�� �� �����ϴ�. ");

        if (_rigidbody == null)
            Debug.LogError($"{gameObject.name}�� ������ٵ� ã�� �� �����ϴ�.");

        if (_circleCollider != null)
        {
            originalColliderOffset = _circleCollider.offset;
            originalColliderSize = _circleCollider.radius;
        }
        else Debug.LogError($"{gameObject.name}�� ��Ŭ�ݶ��̴��� ã�� �� �����ϴ�.");
    }

    void Update()
    {
        if (isDead)
        {
            if (deathCooldown <= 0)
            {
                //���ӿ��� ����
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
        if (collision.gameObject.tag == "BackGround") //�ٴڰ� ��ƾ߸� �ٽ� ���� ����
        {
            canJump = true;
            Debug.Log("���� ����!");
        }

        if (godMode || isDead) return; //�����ų� �׾��ٸ� ��� ����

        if (collision.gameObject.tag == "Obstacle" && invincibleCooldown > 0) return; //��ֹ��̶� �浹�ߴµ� �����ð��̸� ������

        int value = 0;
        Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();

        if (obstacle != null)
        {
            //��ֹ��� ���� �޾� �÷��̾� ü�¿� �ջ��ϴ� �κ�
            value = 000;
            HP += value;
        }

        /* �������� ���� �޾� �÷��̾� ü�¿� �ջ��ϴ� �κ�(������ �߰� ���� �۵��ϵ��� �ּ�ȭ)
        Item item = collision.gameObject.GetComponent<Item>();
        if(item != null)
        {
            value = 000;
            HP += value;
        }
        */
    }
}
