using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    Vector3 velocity;
    float sides = 30.0f;
    float speedMax = 0.3f;
    int shaderColorPropertyID;
    Material myMat;
    Vector3 pos;

    void Start()
    {
        velocity = new Vector3(Random.Range(0.0f, speedMax),
                        Random.Range(0.0f, speedMax),
                        Random.Range(0.0f, speedMax));
        
        shaderColorPropertyID = Shader.PropertyToID("_Color");
        myMat = this.GetComponent<Renderer>().material;
    }

    Color GetRandomColor()
    {
        return new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity);
        pos = transform.position;

        if (Mathf.Abs(pos.x) > sides)
        {
            velocity.x = -velocity.x;
        }
        // if (transform.position.x < -sides)
        // {
        //     velocity.x = -velocity.x;
        // }
        if (Mathf.Abs(pos.y) > sides)
        {
            velocity.y = -velocity.y;
        }
        // if (transform.position.y < -sides)
        // {
        //     velocity.y = -velocity.y;
        // }
        if (Mathf.Abs(pos.z) > sides)
        {
            velocity.z = -velocity.z;
        }
        // if (transform.position.z < -sides)
        // {
        //     velocity.z = -velocity.z;
        // }

        myMat.SetColor(shaderColorPropertyID, new Color(pos.x/sides,
                                                pos.y/sides,
                                                pos.z/sides));

    }
}
