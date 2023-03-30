using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private List<GameObject> pooledObject = new List<GameObject>();
    [SerializeField] private GameObject objectToPool;
    [SerializeField] private int amountToPool;

    void Start()
    {
        Save();
    }

    public void Save()
    {
        if (objectToPool == null) return;

        GameObject go;
        for (int i = 0; i < amountToPool; i++)
        {
            go = Instantiate(objectToPool);
            go.transform.parent = this.gameObject.transform;
            go.SetActive(false);
            pooledObject.Add(go);
        }
    }

    public GameObject OnTakeFromPool()
    {
        for (int i = 0; i < pooledObject.Count; i++)
        {
            if (!pooledObject[i].activeInHierarchy)
            {
                GameObject go = pooledObject[i];
                go.SetActive(true);
                return go;
            }
        }

        return null;
    }

    public GameObject OnTakeFromPool(Vector3 position, Quaternion rotation)
    {
        GameObject go = OnTakeFromPool();
        if (go)
        {
            go.transform.position = position;
            go.transform.rotation = rotation;

            return go;
        }

        return null;
    }

    public void OnReturnToPool(GameObject go)
    {
        go.transform.parent = this.gameObject.transform;
        go.SetActive(false);
    }
    
    public async UniTask OnReturnToPool(GameObject go, float time)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(time));

        go.transform.parent = this.gameObject.transform;
        go.SetActive(false);
    }

    public void OnReturnAll()
    {
        foreach (var item in pooledObject)
        {
            OnReturnToPool(item);
        }
    }
}