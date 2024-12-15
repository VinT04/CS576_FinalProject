using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMessage : MonoBehaviour
{

    private double life = 0;
    private bool active = false;

    private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            active = true;
            life = Math.Round(Time.time, 2);
        }
        // TODO: implement once number of scrolls is tracked
        // text.text = "You lasted " + life + " seconds, collecting X/5 scrolls";
        text.text = "Total duration: " + life + " seconds.";
    }
}
