using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetComponentTest : MonoBehaviour
{
    Transform test;
    [SerializeField] int numOfTests = 5000;

    void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            PerformGetComponent1();
        }
        if(Input.GetKey(KeyCode.Alpha2))
        {
            PerformGetComponent2();
        }
        if(Input.GetKey(KeyCode.Alpha3))
        {
            PerformGetComponent3();
        }
    }

    void PerformGetComponent1()
    {
        for(int i = 0; i < numOfTests; i++)
        {
            test = GetComponent<Transform>();
        }
    }

    void PerformGetComponent2()
    {
        for(int i = 0; i < numOfTests; i++)
        {
            test = (Transform)GetComponent("Transform");
        }
    }

    void PerformGetComponent3()
    {
        for(int i = 0; i < numOfTests; i++)
        {
            test = (Transform)GetComponent(typeof(Transform));
        }
    }

}
