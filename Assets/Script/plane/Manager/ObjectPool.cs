using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    [System.Serializable]
    public class PoolConfig
    {
        public string tag;           // "IceCube" 或 "FireBall"
        public GameObject prefab;    // 对应的预制体
        public int initialSize = 10; // 初始池大小
    }

    public List<PoolConfig> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        Instance = this;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // 初始化每个对象池
        foreach (var pool in pools)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.initialSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.SetParent(transform);
                objectQueue.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectQueue);
        }
    }

    public GameObject Get(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"对象池不存在: {tag}");
            return null;
        }

        // 如果池为空，动态扩容
        if (poolDictionary[tag].Count == 0)
        {
            // 找到对应的预制体
            var config = pools.Find(p => p.tag == tag);
            if (config != null)
            {
                GameObject newObj = Instantiate(config.prefab);
                newObj.SetActive(false);
                newObj.transform.SetParent(transform);
                poolDictionary[tag].Enqueue(newObj);
            }
        }

        GameObject objToGet = poolDictionary[tag].Dequeue();
        objToGet.transform.position = position;
        objToGet.transform.rotation = rotation;
        objToGet.SetActive(true);

        return objToGet;
    }

    public void Return(string tag, GameObject obj)
    {
        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
    }
}