using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public Transform pos1, pos2;
    public float originalSpeed;
    public float speed;
    public GameObject Robotico;
    private Rigidbody2D rb;
    public Vector2 Velocity;
    Vector3 nextPos;
    public bool vertical;

    void Start()
    {
        nextPos = pos1.position;
        rb = GetComponent<Rigidbody2D>();
        if(!vertical)
         rb.velocity = new Vector2(speed, 0);
        else
         rb.velocity = new Vector2(0, speed);

        Velocity = rb.velocity;

        speed = originalSpeed;
    }

    void Update()
    {
        Robotico.GetComponent<PlayerController>().platformVelocity = Velocity;
        if (!vertical)
        {
            if (Vector3.Distance(transform.position, pos1.position) <= 0.1f)
            {
                rb.velocity = new Vector2(-speed, 0);
                Velocity = rb.velocity;
            }
            if (Vector3.Distance(transform.position, pos2.position) <= 0.1f)
            {
                rb.velocity = new Vector2(speed, 0);
                Velocity = rb.velocity;
            }
        }
        if (vertical)
        {
            if (Vector3.Distance(transform.position, pos2.position) <= 0.15f)
            {
                rb.velocity = new Vector2(0, speed); //gdhintjbf
                Velocity = rb.velocity;
            }
            if (Vector3.Distance(transform.position, pos1.position) <= 0.15f)
            {
                rb.velocity = new Vector2(0, -speed);
                Velocity = rb.velocity;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
