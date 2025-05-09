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
    public ObjectType ObjectType => _objectType;

    private string _objectName;
    public string ObjectName => _objectName;

    [SerializeField] ItemType _itemType;

    private void Start()
    {
        _objectName = gameObject.name;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Triggerd : {_objectName}");

        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            switch (_itemType)
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
                        player.Speed += 10f;
                    }
                    break;
                case ItemType.SpeedDown:
                    {
                        Debug.Log("SpeedDown");
                        player.Speed -= 10f;
                    }
                    break;
                case ItemType.Heal:
                    {
                        Debug.Log("Heal");
                        const float healAmount = 10.0f;
                        player.Hp += healAmount;
                    }
                    break;
                default:
                    {
                        Debug.Log("Item");
                    }
                    break;
            }
        }
    }
}