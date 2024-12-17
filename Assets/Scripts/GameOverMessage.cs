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
    private double life;
    public Scrolls scroll; 
    internal double start;

    private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        life = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            active = true;
            life = Math.Round(Time.time - start, 2);
        }
        // TODO: implement once number of scrolls is tracked
        GetComponent<PlayerAudio>().PlayDeathSFX();
        text.text = "You lasted " + life + " seconds, collecting " +  scroll.index + "/10 scrolls";
        //text.text = "Total duration: " + scroll.index + " seconds.";
    }
}