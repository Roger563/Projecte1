using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    int deathCounter;
    bool dead;

    private float respawnTimer;
    public float OriginalRespawnTimer;

    public Transform checkPoint;

    void Start()
    {
        dead = false;
        respawnTimer = OriginalRespawnTimer;
    }

    void Update()
    {
        if (dead)
            respawnTimer -= Time.deltaTime;

        if (respawnTimer <= 0)
        {
            dead = false;
            respawnTimer = OriginalRespawnTimer;

            gameObject.transform.position = checkPoint.position;
            gameObject.GetComponent<PlayerController>().enabled = true;
            gameObject.GetComponent<Magnetism>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "damage")
        {
            gameObject.GetComponent<PlayerController>().enabled = false;
            gameObject.GetComponent<Magnetism>().enabled = false;

            deathCounter++;

            dead = true;
        }
    }
}
