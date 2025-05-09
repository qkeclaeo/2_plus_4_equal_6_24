using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    static SpawnManager spawnManager;
    public static SpawnManager Instance { get { return spawnManager; } }

    [SerializeField] private int numBgCount = 5;
    [SerializeField] private Transform playerTransform;

    private void Awake()
    {
        spawnManager = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Object 태그가 붙어있는 오브젝트(Item/Obstacle) 활성화
        if (collision.CompareTag("Object"))
        {
            Debug.Log($"Object Activation : {collision.name}");
            collision.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Background 태그가 붙어있는 오브젝트 순환
        if (collision.CompareTag("BackGround"))
        {
            Debug.Log($"Triggerd : {collision.name}");

            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;
            return;
        }
        
        // Object 태그가 붙어있는 오브젝트(Item/Obstacle) 파괴
        if (collision.CompareTag("Object"))
        {
            Debug.Log($"Object Destroy : {collision.name}");
            Destroy(collision.gameObject);
        }
    }
}