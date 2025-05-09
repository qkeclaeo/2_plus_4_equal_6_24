using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;
    CircleCollider2D _circleCollider;

    [Header("Player State")] //�ϴ� �ٸ������� �� ���� ������ public���� ���ܵӴϴ�.

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
            Debug.LogError($"{gameObject.name}�� �ִϸ����͸� ã�� �� �����ϴ�. ");

        if (_rigidbody == null)
            Debug.LogError($"{gameObject.name}�� ������ٵ� ã�� �� �����ϴ�.");

        if (_circleCollider != null)
        {
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
            if(!isSliding)
            {
                transform.position += Vector3.down * 0.25f;
                Debug.Log("��ġ����! �Ʒ���!");
            }
            isSliding = true;
            _circleCollider.radius = 0.25f;
        }
        else if (!isSlide)
        {
            if (isSliding)
            {
                transform.position += Vector3.up * 0.25f;
                Debug.Log("��ġ����! ����!");
            }
            isSliding = false;
            _circleCollider.radius = originalColliderSize;
        }

        _rigidbody.velocity = velocity;
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.tag == "BackGround") //�ٴڰ� ��ƾ߸� �ٽ� ���� ����
        {
            canJump = true;
            Debug.Log("���� ����!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //Ʈ���� ������
    {
        if (collision.gameObject.name == "Opstacle") return;
        Tilemap tilemap = collision.gameObject.GetComponent<Tilemap>(); //Ÿ�ϸ� �޾ƿ���
        if (tilemap != null)
        {
            Vector3 hitPoint = collision.ClosestPoint(transform.position); //�÷��̾� ��ġ�� ����� ��ġ ã��
            Vector3Int cellPosition = tilemap.WorldToCell(hitPoint); //������ġ���� ����ġ ã��
            if (tilemap.HasTile(cellPosition)) //�ش� ���� Ÿ���� �ִٸ�
                tilemap.SetTile(cellPosition, null); //�ش� Ÿ�� �����
        }
    }
}
