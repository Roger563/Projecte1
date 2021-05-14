using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed;
    private float start;
    private float end;

    void Update()
    {
        start = transform.parent.position.x;
        end = transform.parent.position.x + 16f;

        transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
        if(transform.position.x >= end)
        {
            transform.position = new Vector3(start, transform.position.y, transform.position.z);
        }
    }
}
