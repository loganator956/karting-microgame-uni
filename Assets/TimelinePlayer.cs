using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelinePlayer : MonoBehaviour
{
    public string recording = "A";
    void Awake()
    {
        TimelineA.GenerateTimelineElements();
        TimelineB.GenerateTimelineElements();
        TimelineC.GenerateTimelineElements();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    int currentIndex = 0;
    int slowDownTimer = 0;

    public int SlowDownLengthCounter = 0;

    void FixedUpdate()
    {
        if (SlowDownLengthCounter > 0)
        {
            if (slowDownTimer == 0)
            {
                slowDownTimer = 1;
            }
            else
            {
                slowDownTimer--;
                currentIndex++;
            }
        }
        else
        {
            currentIndex++;
        }
        switch (recording)
        {
            case "A":
                if (currentIndex >= TimelineA.elements.Count) { currentIndex = 202; };
                transform.position = TimelineA.elements[currentIndex].Position;
                transform.rotation = TimelineA.elements[currentIndex].Rotation;
                break;
            case "B":
                if (currentIndex >= TimelineB.elements.Count) { currentIndex = 161; };
                transform.position = TimelineB.elements[currentIndex].Position;
                transform.rotation = TimelineB.elements[currentIndex].Rotation;
                break;
            case "C":
                if (currentIndex >= TimelineC.elements.Count) { currentIndex = 161; };
                transform.position = TimelineC.elements[currentIndex].Position;
                transform.rotation = TimelineC.elements[currentIndex].Rotation;
                break;
        }
    }
}
