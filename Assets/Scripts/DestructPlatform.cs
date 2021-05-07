using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructPlatform : MonoBehaviour
{
    private float destroyTimmer;
    public float destroyOriginalTimmer;
    private float regenerateTimmer;
    public float regenerateOriginalTimmer;
    private bool destroy = false;
    private bool regenerate = false;
    public Animator animator;
    public GameObject particles;
    bool notSpawned = true;
    

    void Start()
    {
        destroyTimmer = destroyOriginalTimmer;
        regenerateTimmer = regenerateOriginalTimmer;
    }

    void Update()
    {
        Destroy();
        Regenerate();
        animator.SetBool("Destroy", destroy);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            destroy = true;
        }
    }

    void Destroy()
    {
        if (destroy == true)
        {
            destroyTimmer -= Time.deltaTime;
        }
        if (destroyTimmer <= 0.15&& notSpawned)
        {
            Instantiate(particles, gameObject.transform);
            notSpawned = false;
        }
        if (destroyTimmer <= 0)
        {
            destroy = false;
            regenerate = true;
            destroyTimmer = destroyOriginalTimmer;
            notSpawned = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            
        }
    }

    void Regenerate()
    {
        if (regenerate == true)
        {
            regenerateTimmer -= Time.deltaTime;
        }
        if(regenerateTimmer <= 0)
        {
            regenerate = false;
            regenerateTimmer = regenerateOriginalTimmer;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}


       