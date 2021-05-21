using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlatform : MonoBehaviour
{
    public bool standing;
    public GameObject player;
    public Animator animator;

    void Update()
    {
        if (!player.GetComponent<Death>().dead)
        {
            animator.SetBool("movingPlatform", standing);
        }
        else
        {
            animator.SetBool("movingPlatform", false);
            standing = false;
        }
        player.GetComponent<PlayerController>().StandingOnPlatform = standing;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            standing = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            standing = false;
        }
    }
}
