using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolls : MonoBehaviour
{
    private Animator animation_controller;
    private Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "The Adventurer Blake Variant")
        {
            if (anim.isPlaying)
            {
                return;
            }
            anim.Play("spin");
            animation_controller.Play("ScrollAnimation");
        }
    }
}
