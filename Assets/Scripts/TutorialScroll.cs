using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialScroll : MonoBehaviour
{
    public TMP_Text title_text;
    public TMP_Text scroll_text;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Scroll"))
        {
            title_text.text = "Journey to the Treasure!";
            scroll_text.text = "Step through the portal, enter the pyramid if you dare, and claim the riches. But be warned of the mummies that lie within!";
            canvas.SetActive(true);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.name.Contains("Cylinder.013"))
        {
            SceneManager.LoadScene("Intro_outdoor");
        }
    }
}
