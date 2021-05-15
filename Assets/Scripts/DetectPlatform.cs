using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlatform : MonoBehaviour
{
    public bool standing;
    public GameObject Player;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("movingPlatform", standing);
        Player.GetComponent<PlayerController>().StandingOnPlatform = standing;
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
