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
        player = GetComponent<Player>();        
        _objectName = gameObject.name;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player")) return;
        player = collider.GetComponent<Player>();
        Debug.Log("Triggered");
        switch (_objectType)
        {
            case ObjectType.Coin:
                {
                    player.Coin();
                }
                break;
            case ObjectType.SpeedUp:
                {
                    player.ChangeSpeed(10.0f);
                }
                break;
            case ObjectType.SpeedDown:
                {
                    player.ChangeSpeed(-10.0f);
                }
                break;
            case ObjectType.Heal:
                {
                    player.ChangeHp(player.MaxHp * 0.2f);
                }
                break;
            case ObjectType.NormalObstacle:
                {
                    player.ChangeHp(-10.0f);
                }
                break;
            case ObjectType.Arrow:
                {
                    player.ChangeHp(-10.0f);
                }
                break;
            case ObjectType.EndPoint:
                {
                    player.EndPoint();
                }
                break;
            default:
                {
                    Debug.Log("Item");
                }
                break;
        }

        gameObject.SetActive(false);
    }

    private void MoveArrow(GameObject gameObject)
    {
        const float arrowSpeed = 5.0f;
        gameObject.transform.position += arrowSpeed * Time.deltaTime * Vector3.left;
    }
}