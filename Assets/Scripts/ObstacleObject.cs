using UnityEngine;

public class ObstacleObject : Object
{
    enum ObstacleType
    {
        Normal,
        Arrow,
        EndPoint
    }

    private const float normalDamage = 10.0f;
    private const float arrowDamage = 10.0f;

    private ObjectType objectType = ObjectType.Obstacle;
    public ObjectType ObjectType { get => objectType; }
    private string objectName;
    public string ObjectName { get => objectName; }

    [SerializeField] ObstacleType obstacleType;

    private void Start()
    {
        objectName = gameObject.name;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        // SpawnManager와 Arrow가 닿으면 날아옴. 확장성 고려해서 switch 문으로 구현.
        if (collision.CompareTag("SpawnManager"))
        {
            switch(obstacleType)
            {
                case ObstacleType.Arrow:
                    {
                        Debug.Log("SpawnManager Arrow");
                        MoveArrow();
                        break;
                    }
                default:
                    {
                        Debug.Log("SpawnManager Default");
                        break;
                    }
            }
        }

        if (collision.tag != "Player") return;
        
        Debug.Log($"Triggerd : {objectName}");
        switch(obstacleType)
        {
            case ObstacleType.Normal:
                {
                    Debug.Log("Normal");
                    //ChangePlayerHp(-normalDamage);
                }
                break;
            case ObstacleType.Arrow:
                {
                    Debug.Log("Arrow");
                    //ChangePlayerHp(-arrowDamage);
                }
                break;
            case ObstacleType.EndPoint:
                Debug.Log("EndPoint");
                EndPoint();
                break;
            default:
                {
                    Debug.Log("Obstacle");
                }
                break;
        }
    }

    private void MoveArrow()
    {
        float arrowSpeed = 5.0f;

        transform.position += arrowSpeed * Time.deltaTime * Vector3.left;
    }
    void EndPoint()
    {
        GameManager.Instance.GameOver();
    }
}
