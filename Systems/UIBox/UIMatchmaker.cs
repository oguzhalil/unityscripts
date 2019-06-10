using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIMatchmaker : UIElement
{
    public Text remoteNickname;
    public Text timerLabel;

    public bool searching;

    int dots = 0;

    float dotInterval = .2f;

    int maxDots = 4;

    float dotTimer;
    StringBuilder stringBuilder = new StringBuilder();

    float timer;
    int searchTime;

    void Start()
    {
        dotTimer = Time.time + dotInterval;
    }

    void OnEnable()
    {
        timer = Time.time + 1;
        searchTime = 0;
        timerLabel.text = (searchTime / 60).ToString("D2") + ":" + (searchTime % 60).ToString("D2");
    }

    void OnDisable()
    {

    }

    void Update()
    {
        return;

        if (searching)
        {
            if (Time.time > dotTimer)
            {
                if (dots == maxDots)
                {
                    dots = 0;

                }

                dots++;

                stringBuilder.Append('.', dots);

                remoteNickname.text = "SEARCHING" + new string('.', dots);


                dotTimer = Time.time + dotInterval;
            }

            if(Time.time > timer)
            {
                timer = Time.time + 1;

                ++searchTime;
                timerLabel.text = timerLabel.text = (searchTime / 60).ToString("D2") + ":" + (searchTime % 60).ToString("D2");

            }

        }
    }

    public override Type DerivedType()
    {
        return GetType();
    }
}
