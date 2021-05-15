using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopPlatform : MonoBehaviour
{
    public GameObject platform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "damage")
        {
            platform.GetComponent<Rigidbody2D>().velocity =  Vector2.zero;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            platform.GetComponent<Rigidbody2D>().velocity = new Vector2( 0, -platform.GetComponent<movingPlatform>().originalSpeed);
        }
    }
}
