using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagTesting : MonoBehaviour
{
    public int numOfTests = 500000;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TagEqualsPerform();
            TagComparePerform();
        }
    }

    private void TagComparePerform()
    {
        for (int i = 0; i < numOfTests; i++)
        {
            if (this.CompareTag("Test"))
            {
            }
        }
    }

    private void TagEqualsPerform()
    {
        for (int i = 0; i < numOfTests; i++)
        {
            if (this.tag == "Test")
            {
            }
        }
    }
}
