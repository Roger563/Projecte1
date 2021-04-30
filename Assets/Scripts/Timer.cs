﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    private TMP_Text timerTxt;
    float timer=0f;
    public bool runTimer = true;
    // Start is called before the first frame update
    void Start()
    {
        timerTxt = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (runTimer)
        {
            RunTimer();
        }
    }
    void RunTimer() {
       
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        int milliseconds = Mathf.FloorToInt((timer * 100f) % 100f);
        timerTxt.text =(minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00")); 
    } 
    public void ResetTimer()
    {   
        timer = 0f;
    }
}
