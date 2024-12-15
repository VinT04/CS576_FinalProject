using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialScroll : MonoBehaviour
{
    public TMP_Text title_text;
    public TMP_Text scroll_text;
    public GameObject canvas;
    private int index = 0;

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
            title_text.text = titles[index];
            scroll_text.text = facts[index];
            index += 1;
            canvas.SetActive(true);
            Destroy(other.gameObject);
        }
    }
}
