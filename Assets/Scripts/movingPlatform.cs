using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed;
    public GameObject Robotico;
    private Rigidbody2D rb;

    Vector3 nextPos;
    // Start is called before the first frame update
    void Start()
    {
        nextPos=pos1.position;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, pos1.position) <= 0.1f)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        if(Vector3.Distance(transform.position, pos2.position) <= 0.1f)
        {
            rb.velocity = new Vector2(-speed, 0);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
    
   
}
