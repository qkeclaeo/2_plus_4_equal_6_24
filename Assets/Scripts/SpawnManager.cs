using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [SerializeField] private float offsetX;
    [SerializeField] private GameObject[] mapPrefabs;
    public bool IsInfinite { get; private set; }

    Queue<GameObject> mapsQueue;
    List<int> prefabIndex;
    float nextPosX = 0f;
    int mapCount = 0;

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
        nextPosX = mapCount = 0;
        foreach (var obj in mapsQueue)
        {
            obj.SetActive(true);
            ActiveChildObjects(obj);
            obj.transform.position = new Vector3(nextPosX, 0, 0);
            nextPosX += offsetX;
        }
    }
    void GameOver()
    {
        foreach (var obj in mapsQueue)
        {
            obj.SetActive(false);
        }
    }
    public void SetInfiniteMode(bool istrue)
    {
        if (!istrue)
        {
            GameOver();
        }
        IsInfinite = istrue;
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
        if (!IsInfinite) return;

        if (collision.CompareTag("Map"))
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
                    ActiveChildObjects(temp[prefabIndex[i]]);
                    temp[prefabIndex[i]].transform.position = new Vector3(nextPosX, 0, 0);
                    nextPosX += offsetX;
                    mapsQueue.Enqueue(temp[prefabIndex[i]]);
                }
                mapCount = 0;
            }
        }
    }
    void ActiveChildObjects(GameObject obj)
    {
        Transform[] childs = obj.GetComponentsInChildren<Transform>(true);
        foreach (var child in childs)
        {
            child.gameObject.SetActive(true);
        }
    }
}