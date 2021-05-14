using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingEdge : MonoBehaviour
{
    public GameObject player;
    public GameObject movingPlat;
    private bool dead;
    public Vector3 start;

    void Start()
    {
        start = movingPlat.transform.position;
    }

    void Update()
    {
        if(!player.GetComponent<Death>().dead && dead)
        {
            movingPlat.GetComponent<movingPlatform>().speed = 0;
            movingPlat.transform.position = start;
        }

        dead = player.GetComponent<Death>().dead;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            movingPlat.GetComponent<movingPlatform>().enabled = true;
            movingPlat.GetComponent<movingPlatform>().speed = movingPlat.GetComponent<movingPlatform>().originalSpeed;
        }
    }
}
