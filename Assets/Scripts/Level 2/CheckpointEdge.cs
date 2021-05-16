using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointEdge : MonoBehaviour
{
    public GameObject startingEdge;
    public GameObject empty;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            startingEdge.GetComponent<StartingEdge>().start = empty.transform.position;
        }
    }
}
