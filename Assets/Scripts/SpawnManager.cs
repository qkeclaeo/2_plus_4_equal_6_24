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
        Debug.Log("Triggerd: " + collision.name);

        if (collision.CompareTag("BackGround"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;
            return;
        }
    }    
}