using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    static SpawnManager spawnManager;
    public static SpawnManager Instance { get { return spawnManager; } }

    [SerializeField] private int numBgCount = 5;

    private void Awake()
    {
        spawnManager = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BackGround"))
        {
            Debug.Log($"Triggerd : {collision.name}");

            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.activeSelf)
        {
            collision.gameObject.SetActive(true);
        }
        else
        {
            Destroy(collision.gameObject);
        }        
    }
}