﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelectedFollow : MonoBehaviour
{
    public GameObject mainCam;

    void Start()
    {
        //transform.position = new Vector3(mainCam.transform.position.x, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        transform.position = new Vector3(mainCam.transform.position.x, transform.position.y, transform.position.z);
    }
}
