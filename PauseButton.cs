using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    private Image image;
    public GameObject clock;
    private Timer timer;
    // Update is called once per frame
    void Start()
    {
        timer = clock.GetComponent<Timer>();
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            timer.setGameSpeed(0f);
        }

        if (Time.timeScale == 0f)
        {
            image.color = new Color(0f, 0f, 0f, 1f);
        }
        else
        {
            image.color = new Color(1f, 1f, 1f, 1f);
        }

    }
}
