using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetism : MonoBehaviour
{

    public bool magnetism = false;

    private Rigidbody2D rb;
    private Transform point;

    private List<GameObject> attractors = new List<GameObject>();
    private List<GameObject> repelors = new List<GameObject>();

    public float attractorCoefX;
    public float attractorCoefY;
    public float repelorCoefX;
    public float repelorCoefY;

    public float maxSpeed;

    public float counterVelocityCoefX;
    public float velocityBoostX;
    public float velocityBoostY;
    private bool wasFalling = false;
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
                Attract(ClosestAttractor());
            }
        }
        else {
            wasFalling = false;
            magnetism = false;
        }
        if (Input.GetButton("Fire2"))
        {
            if (repelors.Count == 0)
            {
                //fail
            }
            else
            {
                Repel(ClosestRepelor());
            }

        }
        else
        {
          
        }
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed,maxSpeed), Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed));
       
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

    Vector2 ClosestAttractor()
    {
        if (attractors.Count == 1) return attractors[0].transform.position;

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

    Vector2 ClosestRepelor()
    {
        Vector2 smallest = new Vector2();
        smallest = repelors[0].transform.position - gameObject.transform.position;

        int index = 0;
        int closest = 0;
        foreach (GameObject point in repelors) //if there are two or more magnets in range
        {
            Vector2 distance = point.transform.position - gameObject.transform.position;
            if (distance.magnitude < smallest.magnitude)
            {
                closest = index;
            }
            index++;
        }


        return repelors[closest].transform.position;
    }

    void Attract(Vector2 magnet)
    {
        Vector2 direction = new Vector2();
        direction = magnet - (Vector2)gameObject.transform.position;
        direction.Normalize();
        float atractspeedX = (direction.x * attractorCoefX) * Time.deltaTime;
        float atractspeedY = (direction.y * attractorCoefY) * Time.deltaTime;
        float boostedAtractSpeedY = (direction.y * attractorCoefY * velocityBoostY) * Time.deltaTime;
        if (rb.velocity.x > 0)
        {
            
            rb.velocity = new Vector2(rb.velocity.x + Mathf.Clamp(atractspeedX, -atractspeedX/ counterVelocityCoefX, atractspeedX), rb.velocity.y);

        }
        else if (rb.velocity.x < 0)
        {
            
            rb.velocity = new Vector2(rb.velocity.x + Mathf.Clamp(atractspeedX, atractspeedX, -atractspeedX / counterVelocityCoefX), rb.velocity.y) ;

        }
        else
            rb.velocity = new Vector2(rb.velocity.x + atractspeedX, rb.velocity.y);
        if (rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + boostedAtractSpeedY);
            wasFalling = true;

        }
       else if (rb.velocity.y > 0 && wasFalling)
        {
            if(rb.velocity.x>0)
            rb.velocity = new Vector2(rb.velocity.x + (velocityBoostX * Time.deltaTime ), rb.velocity.y +atractspeedY);
            else
                rb.velocity = new Vector2(rb.velocity.x - (velocityBoostX * Time.deltaTime), rb.velocity.y + atractspeedY);

        }
        else if (rb.velocity.y >0)
        {
            rb.velocity = new Vector2(rb.velocity.x , rb.velocity.y + atractspeedY);

        }
        if( rb.velocity.y==0)
            rb.velocity = new Vector2(rb.velocity.x , rb.velocity.y + atractspeedY);
    }
    void Repel(Vector2 magnet)
    {
        Vector2 direction = new Vector2();

        direction = magnet-(Vector2)gameObject.transform.position ;
        direction.Normalize();

        rb.velocity = new Vector2(rb.velocity.x - (direction.x * repelorCoefX) * Time.deltaTime, rb.velocity.y - (direction.y * repelorCoefY) * Time.deltaTime);
    }
    void OnCollisionEnter2D (Collision2D col){
        if ( (col.collider.tag == "attractors"  && Input.GetButton("Fire1")) || (col.collider.tag == "attractAndRepel" && Input.GetButton("Fire1"))) {
            magnetism = true;
        }
    
    }
 
}

