using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Timer : MonoBehaviour
{
    public GameObject tree;

    private float hourInGameSeconds = 60;
    private float currentHour;
    private float gameSpeed;
    private bool done = true;
    // Start is called before the first frame update
    //Gonna change to onAwake eventually
    void Start()
    {
        currentHour = 8;
        gameSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
        if (Time.fixedTime % hourInGameSeconds == 0 && done == true)
        {
            incrementCurrentHour();
            doneFalse();
            Invoke("doneTrue", 5);
            if(currentHour >= 21)
            {
                this.currentHour = 1;
                
            }
        }
    }

    public float getCurrentHour()
    {
        return currentHour;
    }

    public void setGameSpeed(float gameSpeed)
    {
        this.gameSpeed = gameSpeed;  
    }

    public void incrementCurrentHour()
    {
        currentHour++;
    }

    public void doneFalse()
    {
        done = false;
    }

    public void doneTrue()
    {
        done = true;
    }
}
