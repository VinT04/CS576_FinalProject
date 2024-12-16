using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Minimap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        bool active = true;
        string curr = SceneManager.GetActiveScene().name;
        if (curr == "Room")
        {
            active = false;
        }
        gameObject.SetActive(active);
    }
}
