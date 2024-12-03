using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Text volume_text;
    public Slider volume_slider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float val = volume_slider.value;
        volume_text.text = val.ToString("0");
        AudioListener.volume = val / 100;
    }

    public void NewGame()
    {
        // SceneManager.LoadScene();
    }

    public void LoadGame()
    {
        // SceneManager.LoadScene();
    }

    public void OptionsMenu()
    {
        // SceneManager.LoadScene();
    }
}
