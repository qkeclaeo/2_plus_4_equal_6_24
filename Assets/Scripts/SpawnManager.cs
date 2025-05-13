using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private float offsetX;
    float nextPosX = 0f;
    public static SpawnManager Instance { get; private set; }

    public GameObject[] mapPrefabs;

    public Transform spawnPoint;
    //    private Transform lastMapPos = null;

    Queue<GameObject> mapsQueue;
    //List<GameObject> mapsPool;
    List<int> prefabIndex;

    int mapCount = 0;

    bool isInfinite = false;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        mapsQueue = new Queue<GameObject>();
    }

    void Start()
    {
        prefabIndex = new List<int>();
        for (int i = 0; i < mapPrefabs.Length; ++i)
        {
            prefabIndex.Add(i);
        }
        for (int i = 0; i < 2; ++i) {
            ShuffleIndex();
            for (int j = 0; j < mapPrefabs.Length; ++j)
            {
                GameObject obj = Instantiate(mapPrefabs[prefabIndex[j]]);
                obj.transform.position = new Vector3(nextPosX, 0, 0);
                nextPosX += offsetX;
                obj.SetActive(false);
                mapsQueue.Enqueue(obj);
            }
        }
    }
    public void Init()
    {
        nextPosX = 0;
        foreach (var obj in mapsQueue)
        {
            obj.SetActive(true);
            obj.transform.position = new Vector3(nextPosX, 0, 0);
            nextPosX += offsetX;
        }
    }
    public void GameOver()
    {
        foreach (var obj in mapsQueue)
        {
            obj.SetActive(false);
        }
    }
    public void SetInfiniteMode(bool istrue)
    {
        isInfinite = istrue;
    }
    void ShuffleIndex()
    {
        for (int i = prefabIndex.Count - 1; i > 0; --i)
        {
            int j = Random.Range(0, i + 1);
            int temp = prefabIndex[j];
            prefabIndex[j] = prefabIndex[i];
            prefabIndex[i] = temp;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isInfinite && collision.CompareTag("MapPrefab"))
        {
            ++mapCount;
            if (mapCount >= mapPrefabs.Length)
            {
                List<GameObject> temp = new List<GameObject>();
                for (int i = 0; i < mapPrefabs.Length; ++i)
                {
                    temp.Add(mapsQueue.Dequeue());
                }
                ShuffleIndex();

                for (int i = 0; i < temp.Count; ++i)
                {
                    //temp[prefabIndex[i]].Init();        //초기화 함수
                    mapsQueue.Enqueue(temp[prefabIndex[i]]);
                }
                mapCount = 0;
            }
        }
    }
}