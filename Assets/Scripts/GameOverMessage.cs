using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMessage : MonoBehaviour
{

    private bool active = false;
    public Scrolls scroll;  

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
        }
        // TODO: implement once number of scrolls is tracked
        // text.text = "You lasted " + life + " seconds, collecting X/5 scrolls";
        text.text = "Total duration: " + scroll.index + " seconds.";
    }
}