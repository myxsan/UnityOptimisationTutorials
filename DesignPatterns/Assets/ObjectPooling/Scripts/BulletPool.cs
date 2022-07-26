using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    static BulletPool instance;
    
    public GameObject objPrefab;
    public int poolSize = 10000000;

    GameObject[] pool;
    int currentPoolIndex = 0;

    private void Awake()
    {
        if(instance != null  && instance != this)
        {
            Destroy(gameObject);
            return;
        }else
            instance = this;

        pool = new GameObject[poolSize];
        for(int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(objPrefab, instance.transform) as GameObject;
            pool[i].SetActive(false);
        }
    }
    public static void Take(Vector3 position, Quaternion rotation)
    {
        if(++instance.currentPoolIndex >= instance.pool.Length)
            instance.currentPoolIndex = 0;

        instance.pool[instance.currentPoolIndex].SetActive(false);
        instance.pool[instance.currentPoolIndex].transform.position = position;
        instance.pool[instance.currentPoolIndex].transform.rotation = rotation;
        instance.pool[instance.currentPoolIndex].SetActive(true);
    }
}
