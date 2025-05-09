using UnityEngine;

public class ObstacleObject : Object
{
    enum ObstacleType
    {
        Normal,
        Arrow,
        EndPoint
    }

    private const float _normalDamage = 10.0f;
    private const float _arrowDamage = 10.0f;

    private ObjectType _objectType = ObjectType.Obstacle;
    public ObjectType ObjectType { get => _objectType; }
    private string _objectName;
    public string ObjectName { get => _objectName; }

    [SerializeField] ObstacleType _obstacleType;

    private void Start()
    {
        _objectName = gameObject.name;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        // SpawnManager와 Arrow가 닿으면 날아옴. 확장성 고려해서 switch 문으로 구현.
        if (collision.gameObject.CompareTag("SpawnManager"))
        {
            switch(_obstacleType)
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

        if (!collision.gameObject.CompareTag("Player")) return;
        
        Debug.Log($"Triggerd : {_objectName}");
        switch(_obstacleType)
        {
            case ObstacleType.Normal:
                {
                    Debug.Log("Normal");
                    GameManager.Instance.ChangePlayerHP(-_normalDamage);
                }
                break;
            case ObstacleType.Arrow:
                {
                    Debug.Log("Arrow");
                    GameManager.Instance.ChangePlayerHP(-_arrowDamage);
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

        base.DestroyTile(collision);
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
