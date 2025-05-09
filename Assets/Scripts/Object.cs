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
    private void Start()
    {
        _objectName = gameObject.name;
    }

    // SpawnManager·Î ÀÌ°ü?
    private void MoveArrow(GameObject gameObject)
    {
        const float arrowSpeed = 5.0f;
        gameObject.transform.position += arrowSpeed * Time.deltaTime * Vector3.left;
    }
}