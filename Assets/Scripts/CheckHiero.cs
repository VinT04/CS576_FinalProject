using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckHiero : MonoBehaviour
{
    private Text message;
    private float last_time;

    private GameObject escape;
    // Start is called before the first frame update
    void Start()
    {
        message = GameObject.Find("T").GetComponent<Text>();
        message.text = "";

        escape = GameObject.Find("Continue");
        escape.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > last_time + 1.5 && message.text != "Correct!")
        {
            message.text = "";
        }
    }

    public void checkResponse(string answer)
    {
        if (answer == "!")
        {
            message.text = "Correct!";
            escape.SetActive(true);
        }
        else
        {
            Debug.Log("Incorrect");
            message.text = "Incorrect";
        }
        last_time = Time.time;
    }
}
