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
                    Coin();
                }
                break;
            case ItemType.SpeedUp:
                {
                    Debug.Log("SpeedUp");
                    SpeedUp();
                }
                break;
            case ItemType.SpeedDown:
                {
                    Debug.Log("SpeedDown");
                    SpeedDown();
                }
                break;
            case ItemType.Heal:
                {
                    Debug.Log("Heal");
                    Heal();
                }
                break;
            default:
                {
                    Debug.Log("Item");
                }
                break;
        }
    }

    private void Coin()
    {
        GameManager.Instance.UpdateScore(1);
    }

    private void SpeedUp()
    {
        // 속도 증가
        // player.speed += 10;
    }

    private void SpeedDown()
    {
        // 속도 감소
        // player.speed -= 10;
    }

    private void Heal()
    {
        const float healAmount = 10.0f;

        ChangePlayerHp(player, healAmount);
    }
}