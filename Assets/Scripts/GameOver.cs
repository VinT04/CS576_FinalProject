using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text scrollCount;
    public bool seen = false;
    // Start is called before the first frame update
    void Start()
    {
        scrollCount = GetComponent<Text>();
    }

    public void updateText()
    {
        int keysCollected = PlayerPrefs.GetInt("KeysCollected", 0);
        scrollCount.text = "You collected " + keysCollected + " / 10 scrolls";
    }

    public void resetKeys()
    {
        PlayerPrefs.SetInt("KeysCollected", 0);
        seen = false;
    }

    void Update()
    {
        if (!seen)
        {
            updateText();
            seen = true;
        }
    }

}
