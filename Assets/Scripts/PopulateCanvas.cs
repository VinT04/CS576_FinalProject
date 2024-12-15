using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateCanvas : MonoBehaviour
{

    public void populate()
    {
        Canvas[] canvases = FindObjectsOfType<Canvas>(true);
        int j = 0;
        for (int i = 0; i < canvases.Length; i++)
        {
            if (canvases[i].name == "Canvas")
            {
                canvases[i].gameObject.SetActive(true);
                return;
            }
            else if (canvases[i].name == "Canvas-temp")
            {
                j = i;
            }
        }
        for (int i = 0; i < canvases.Length; i++)
        {
            if (canvases[i].name == "Canvas-Temp")
            {
                canvases[i].gameObject.SetActive(true);
                return;
            }
        }
        canvases[j].gameObject.SetActive(true);
    }
}
