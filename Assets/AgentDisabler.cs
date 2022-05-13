using System;
using UnityEngine;
using KartGame.KartSystems;

public class AgentDisabler : MonoBehaviour
{
    float timer = 0f;
    void Start()
    {

    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GetComponent<ArcadeKart>().enabled = true;
                GetComponent<BaseInput>().enabled = true;
            }
        }
    }

    public void Disable()
    {
        timer = 5f;
        GetComponent<ArcadeKart>().enabled = false;
        GetComponent<BaseInput>().enabled = false;
    }

}