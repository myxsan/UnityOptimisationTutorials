using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactPool : MonoBehaviour
{
    static ImpactPool instance;
    
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
            if(pool[i].activeSelf == true)
                pool[i].SetActive(false);
        }
    }
    public static void Take(Vector3 pos, Quaternion rot)
    {
        if(++instance.currentPoolIndex >= instance.pool.Length)
            instance.currentPoolIndex = 0;

        instance.pool[instance.currentPoolIndex].SetActive(false);
        instance.pool[instance.currentPoolIndex].transform.position = pos;
        instance.pool[instance.currentPoolIndex].transform.rotation = rot;
        instance.pool[instance.currentPoolIndex].SetActive(true);
    }
}
