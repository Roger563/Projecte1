﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSetter : MonoBehaviour
{
    public GameObject checkPoints;

    void Awake()
    {
        checkPoints = GameObject.FindGameObjectWithTag("Checkpoint");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            gameObject.GetComponent<Death>().checkPoint = collision.transform.parent.GetComponent<Transform>();
            collision.gameObject.GetComponent<Animator>().SetBool("On", true);
        }
    }
}
