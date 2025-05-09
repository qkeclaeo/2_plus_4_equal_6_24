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

    #region 충돌 관련 메서드
    public void Coin()
    {
        Debug.Log("Coin");
        GameManager.Instance.UpdateScore(1);
    }

    public void SpeedUp()
    {
        Debug.Log("SpeedUp");
        GameManager.Instance.ChangePlayerSpeed(10f);
    }

    public void SpeedDown()
    {
        Debug.Log("SpeedDown");
        GameManager.Instance.ChangePlayerSpeed(-10f);
    }

    public void Heal()
    {
        const float healAmount = 10.0f;

        Debug.Log("Heal");
        GameManager.Instance.ChangePlayerHP(healAmount);
    }

    public void NormalObstacle()
    {
        const float _normalDamage = 10.0f;

        Debug.Log("NormalObstacle");
        GameManager.Instance.ChangePlayerHP(-_normalDamage);
    }

    public void Arrow()
    {
        const float _ArrowDamage = 10.0f;

        Debug.Log("Arrow");
        GameManager.Instance.ChangePlayerHP(-_ArrowDamage);
    }

    public void EndPoint()
    {
        Debug.Log("EndPoint");
        GameManager.Instance.GameOver();
    }

    #endregion

    // SpawnManager로 이관?
    private void MoveArrow(GameObject gameObject)
    {
        const float arrowSpeed = 5.0f;

        gameObject.transform.position += arrowSpeed * Time.deltaTime * Vector3.left;
    }
}