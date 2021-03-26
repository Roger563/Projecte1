using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetism : MonoBehaviour
{

    public bool magnestism = false;

    private Rigidbody2D rb;
    private Transform point;

    private List<GameObject> attractors = new List<GameObject>();
    private List<GameObject> repelors = new List<GameObject>();

    public float magneticCoefX;
    public float magneticCoefY;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            /*
            Vector2 distance = new Vector2(point.position.x - rb.position.x, point.position.y - rb.position.y);
            //distance.Normalize();
            if(distance.magnitude <= iman.GetComponent<CircleCollider2D>().radius)
            {
                attraction();
            }
            */
            if (attractors.Count == 0)
            {
                //fail
            }
            else
            {
                Attract(ClosestGameObj());
            }
        }
        if (Input.GetButton("Fire2"))
        {
            if (repelors.Count == 0)
            {
                //fail
            }

        }
        else
        {
            magnestism = false;
        }
    }

    void attraction()
    {
        magnestism = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "attractors")
        {
            attractors.Add(col.gameObject);
        }
        else if(col.tag == "repelors")
        {
            repelors.Add(col.gameObject);
        }
        else if(col.tag == "attractAndRepel")
        {
            attractors.Add(col.gameObject);
            repelors.Add(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "attractors")
        {
            attractors.Remove(col.gameObject);
        }
        else if (col.tag == "repelors")
        {
            repelors.Remove(col.gameObject);
        }
        else if (col.tag == "attractAndRepel")
        {
            attractors.Remove(col.gameObject);
            repelors.Remove(col.gameObject);
        }
    }

    Vector2 ClosestGameObj()
    {
        Vector2 smallest = new Vector2();
        smallest = attractors[0].transform.position - gameObject.transform.position;

        int index = 0;
        int closest = 0;
        foreach (GameObject point in attractors) //if there are two or more magnets in range
        {
            Vector2 distance = point.transform.position - gameObject.transform.position;
            if (distance.magnitude < smallest.magnitude)
            {
                closest = index;
            }
            index++;
        }

        return attractors[closest].transform.position;
    }

    void Attract(Vector2 magnet)
    {
        Vector2 direction = new Vector2();

        direction = magnet - (Vector2)gameObject.transform.position;
        direction.Normalize();

        rb.velocity = new Vector2(rb.velocity.x + (direction.x * magneticCoefX)*Time.deltaTime, rb.velocity.y + (direction.y * magneticCoefY) *Time.deltaTime);
    }
}
