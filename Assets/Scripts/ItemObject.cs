using UnityEngine;

public class ItemObject : Object
{
    enum ItemType
    {
        Coin,
        SpeedUp,
        SpeedDown,
        Heal
    }    

    private ObjectType _objectType = ObjectType.Item;
    public ObjectType ObjectType { get => _objectType; }
    private string _objectName;
    public string ObjectName { get => _objectName; }

    [SerializeField] ItemType _itemType;

    Player player;    

    private void Start()
    {
        _objectName = gameObject.name;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Triggerd : {_objectName}");

        player = collision.gameObject.GetComponent<Player>();
        if (player == null) { return; }

        switch(_itemType)
        {
            case ItemType.Coin:
                {
                    Debug.Log("Coin");
                    GameManager.Instance.UpdateScore(1);
                }
                break;
            case ItemType.SpeedUp:
                {
                    Debug.Log("SpeedUp");
                    GameManager.Instance.ChangePlayerSpeed(10f);
                }
                break;
            case ItemType.SpeedDown:
                {
                    Debug.Log("SpeedDown");
                    GameManager.Instance.ChangePlayerSpeed(-10f);
                }
                break;
            case ItemType.Heal:
                {
                    Debug.Log("Heal");
                    const float healAmount = 10.0f;

                    GameManager.Instance.ChangePlayerHP(healAmount);
                }
                break;
            default:
                {
                    Debug.Log("Item");
                }
                break;
        }

        base.DestroyTile(collision);
    }
}