using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public int deathCounter;
    public bool dead;

    private float respawnTimer;
    public float OriginalRespawnTimer;

    public Transform checkPoint;
    private float gravScale;

    private CameraShake cam;

    void Awake()//was start
    {
        dead = false;
        respawnTimer = OriginalRespawnTimer;
        gravScale = gameObject.GetComponent<Rigidbody2D>().gravityScale;
        cam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>().GetComponent<CameraShake>();
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
            gameObject.GetComponent<Rigidbody2D>().gravityScale = gravScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "damage")
        {
            gameObject.GetComponent<PlayerController>().enabled = false;
            gameObject.GetComponent<Magnetism>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            cam.StartCoroutine("CamShake");

            deathCounter++;

            dead = true;
        }
    }
}
