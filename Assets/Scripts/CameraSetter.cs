using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetter : MonoBehaviour
{
    public GameObject newCam;
    public GameObject oldCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            oldCam.SetActive(false);
            newCam.SetActive(true);
        }
    }
}
