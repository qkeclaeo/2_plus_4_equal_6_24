using UnityEngine;

public class ObstacleObject : Object
{
    enum ObstacleType
    {
        Normal,
        Arrow
    }

    private const float normalDamage = 10.0f;
    private const float arrowDamage = 10.0f;

    private ObjectType objectType = ObjectType.Obstacle;
    public ObjectType ObjectType { get => objectType; }
    private string objectName;
    public string ObjectName { get => objectName; }

    [SerializeField] ObstacleType obstacleType;

    Player player;

    private void Start()
    {
        objectName = gameObject.name;
    }

    private void Update()
    {
        if (obstacleType == ObstacleType.Arrow)
        {
            if (gameObject.activeSelf)
            {
                MoveArrow();
            }
        }        
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Triggerd : {objectName}");

        player = collision.GetComponent<Player>();
        if (player == null) { return; }

        switch(obstacleType)
        {
            case ObstacleType.Normal:
                {
                    Debug.Log("Normal");
                    Normal();
                }
                break;
            case ObstacleType.Arrow:
                {
                    Debug.Log("Arrow");
                    Arrow();
                }
                break;
            default:
                {
                    Debug.Log("Obstacle");
                }
                break;
        }
    }

    private void Normal()
    {
        ChangePlayerHp(player, -normalDamage);
    }

    private void Arrow()
    {
        ChangePlayerHp(player, -arrowDamage);
    }

    private void MoveArrow()
    {
        float arrowSpeed = 5.0f;

        transform.position += arrowSpeed * Time.deltaTime * Vector3.left;
    }
}
