using UnityEngine;

public class ObstacleObject : Object
{
    enum ObstacleType
    {
        Normal,
        Arrow,
        EndPoint
    }

    private float _normalDamage = 10.0f;
    private float _arrowDamage = 10.0f;

    private ObjectType _objectType = ObjectType.Obstacle;
    public ObjectType ObjectType => _objectType;

    private string _objectName;
    public string ObjectName => _objectName;

    [SerializeField] ObstacleType _obstacleType;

    private void Start()
    {
        _objectName = gameObject.name;
    }

    private void Update()
    {
        if (_obstacleType == ObstacleType.Arrow)
        {
            if (gameObject.activeSelf)
            {
                MoveArrow();
            }
        }        
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Triggerd : {_objectName}");

        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            switch (_obstacleType)
            {
                case ObstacleType.Normal:
                    {
                        Debug.Log("Normal");
                        player.Hp -= _normalDamage;
                    }
                    break;
                case ObstacleType.Arrow:
                    {
                        Debug.Log("Arrow");
                        player.Hp -= _arrowDamage;
                    }
                    break;
                case ObstacleType.EndPoint:
                    {
                        Debug.Log("EndPoint");
                        EndPoint();
                    }
                    break;
                default:
                    {
                        Debug.Log("Obstacle");
                    }
                    break;
            }
        }
    }

    private void MoveArrow()
    {
        float arrowSpeed = 5.0f;

        transform.position += arrowSpeed * Time.deltaTime * Vector3.left;
    }

    private void EndPoint()
    {
        GameManager.Instance.GameOver();
    }
}
