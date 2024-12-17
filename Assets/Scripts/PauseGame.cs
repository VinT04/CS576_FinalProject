using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public GameObject pause_menu;
    public TMP_Text volume_text;
    public Slider volume_slider;

    // Start is called before the first frame update
    void Start()
    {
        float vol = AudioListener.volume * 100;
        volume_text.text = vol.ToString("0");
        volume_slider.value = (int)vol;
    }

    // Update is called once per frame
    void Update()
    {
        float val = volume_slider.value;
        volume_text.text = val.ToString("0");
        AudioListener.volume = val / 100;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause_menu.activeInHierarchy)
            {
                pause_menu.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                pause_menu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
