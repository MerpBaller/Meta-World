using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentTime : MonoBehaviour
{
    public GameObject clock;
    public Text text;
    private Timer timer;

    // Update is called once per frame
    void Start()
    {
        text = GetComponent<Text>();
        timer = clock.GetComponent<Timer>();
    }

    void Update()
    {
        text.text = timer.getCurrentHour().ToString();
    }
}
