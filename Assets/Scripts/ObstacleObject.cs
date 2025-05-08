using UnityEngine;

public class ObstacleObject : Object
{
    private const float obstacleDamage = 10.0f;

    private ObjectType objectType = ObjectType.Obstacle;
    public ObjectType ObjectType { get => objectType; }
    private string objectName;
    public string ObjectName { get => objectName; }

    private void Start()
    {
        objectName = gameObject.name;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Triggerd : {objectName}");

        Player player = collision.GetComponent<Player>();
        if (player == null) { return; }

        ChangePlayerHp(player, -obstacleDamage);
    }
}
