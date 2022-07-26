using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureTesting : MonoBehaviour
{
    const int numberOfTests = 10000;
    int[] inventory = new int[numberOfTests];
    Dictionary<int, int> inventoryD = new Dictionary<int, int>();
    List<int> inventoryL = new List<int>();
    HashSet<int> inventoryH = new HashSet<int>();


    void AddValuesInArray()
    {
        for (int i = 0; i < numberOfTests; i++)
        {
            inventory[i] = i;
        }
    }
    void IterValuesInArray()
    {
        foreach (int i in inventory)
        {
            Debug.Log(inventory[i]);
        }
    }
    void AddValuesInList()
    {
        for (int i = 0; i < numberOfTests; i++)
        {
            inventoryL.Add(i);
        }
    }
    void IterValuesInList()
    {
        foreach (int i in inventoryL)
        {
            Debug.Log(inventoryL[i]);
        }
    }
    void AddValuesInHashSet()
    {
        for (int i = 0; i < numberOfTests; i++)
        {
            inventoryH.Add(i);
        }
    }
     void IterValuesInHash()
     {
        foreach (int i in inventoryH)
        {
            Debug.Log(i);
        }
     }

    void AddValuesInDictionary()
    {
        for (int i = 0; i < numberOfTests; i++)
        {
            inventoryD.Add(i, i);
        }
    }

    void IterValuesInDictionary()
    {
        foreach(KeyValuePair<int,int> i in inventoryD)
        {
            Debug.Log(i.Value);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddValuesInArray();
            AddValuesInDictionary();
            AddValuesInHashSet();
            AddValuesInList();
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            IterValuesInArray();
            IterValuesInDictionary();
            IterValuesInList();
            IterValuesInHash();
        }
    }
}
