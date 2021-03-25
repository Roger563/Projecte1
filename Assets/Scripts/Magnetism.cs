using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetism : MonoBehaviour
{

    public GameObject iman;

    public bool magnestism = false;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButton("Fire1")) {
            attraction();
        }
        else
        {
            magnestism = false;

            iman.SetActive(false);
        }
    }

    void attraction()
    {
        magnestism = true;
        rb.velocity = new Vector2(0.0f, rb.velocity.y);

        iman.SetActive(true);
    }
}
