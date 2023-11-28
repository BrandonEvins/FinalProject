using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool 
{
     private GameObject prefab;
    private List<GameObject> pool;

    public ObjectPool(GameObject prefab, int initialSize)
    {
        this.prefab = prefab;
        pool = new List<GameObject>();

        for (int i = 0; i < initialSize; i++)
        {
            CreateObject();
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        return null;
    }

    private GameObject CreateObject()
    {
        GameObject obj = Object.Instantiate(prefab);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }
}
