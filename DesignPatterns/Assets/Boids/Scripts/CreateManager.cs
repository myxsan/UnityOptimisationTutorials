using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateManager : MonoBehaviour
{
    public GameObject boidPrefab;
    public int numBoids;
    void Start()
    {
        FlockManagerStatic.Init(boidPrefab, numBoids);
    }

}
