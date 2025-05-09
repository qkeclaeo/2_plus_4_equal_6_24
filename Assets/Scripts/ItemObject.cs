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

    private ObjectType objectType = ObjectType.Item;
    public ObjectType ObjectType { get => objectType; }
    private string objectName;
    public string ObjectName { get => objectName; }

    [SerializeField] ItemType itemType;

    Player player;    

    private void Start()
    {
        objectName = gameObject.name;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Triggerd : {objectName}");

        player = collision.GetComponent<Player>();
        if (player == null) { return; }

        switch(itemType)
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
                    GameManager.Instance.ChangePlayerSpeed(3f);
                }
                break;
            case ItemType.SpeedDown:
                {
                    Debug.Log("SpeedDown");
                    GameManager.Instance.ChangePlayerSpeed(-3f);
                }
                break;
            case ItemType.Heal:
                {
                    Debug.Log("Heal");
                    const float healAmount = 10.0f;

                    ChangePlayerHp(healAmount);
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