using UnityEngine;

public enum ObjectType
{
    Coin,
    SpeedUp,
    SpeedDown,
    Heal,
    NormalObstacle,
    Arrow,
    EndPoint
}

public class Object : MonoBehaviour
{
    [SerializeField] private ObjectType _objectType;
    public ObjectType ObjectType { get => _objectType; }
    private string _objectName;
    public string ObjectName { get => _objectName; }

    Player player;

    private void Start()
    {
        _objectName = gameObject.name;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player")) return;

        Debug.Log("Triggered");
        switch (_objectType)
        {
            case ObjectType.Coin:
                {
                    Coin();
                }
                break;
            case ObjectType.SpeedUp:
                {
                    SpeedUp();
                }
                break;
            case ObjectType.SpeedDown:
                {
                    SpeedDown();
                }
                break;
            case ObjectType.Heal:
                {
                    Heal();
                }
                break;
            case ObjectType.NormalObstacle:
                {
                    NormalObstacle();
                }
                break;
            case ObjectType.Arrow:
                {
                    Arrow();
                }
                break;
            case ObjectType.EndPoint:
                {
                    EndPoint();
                }
                break;
            default:
                {
                    Debug.Log("Item");
                }
                break;
        }
    }

    public void Coin()
    {
        Debug.Log("Coin");
        GameManager.Instance.UpdateScore(1);
    }

    public void SpeedUp()
    {
        Debug.Log("SpeedUp");
        player.Speed += 10f;
    }

    public void SpeedDown()
    {
        Debug.Log("SpeedDown");
        player.Speed -= 10f;
    }

    public void Heal()
    {
        Debug.Log("Heal");
        player.Hp += player.MaxHp * 0.2f;
    }

    public void NormalObstacle()
    {
        /** isStun과 invincibleCooldown 보호수준 설정 후 주석 해제
        Debug.Log("NrmalObstacle");
        if (!player._isStun && player._invincibleCooldown == 0)
        {
            player.Hp -= 10f;
            StartCoroutine(PlayerStun());
        }*/
    }

    public void Arrow()
    {
        /** 
        Debug.Log("ArrowObstacle");
        if (!_isStun && _invincibleCooldown == 0)
        {
            player.Hp -= 10f;
            StartCoroutine(PlayerStun());
        }*/
    }

    public void EndPoint()
    {
        Debug.Log("EndPoint");
        GameManager.Instance.GameOver();
    }

    private void MoveArrow(GameObject gameObject)
    {
        const float arrowSpeed = 5.0f;
        gameObject.transform.position += arrowSpeed * Time.deltaTime * Vector3.left;
    }
}