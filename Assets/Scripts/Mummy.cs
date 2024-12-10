using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mummy : MonoBehaviour
{

    private Pyramid pyramid;
    private GameObject player;
    private int radius;
    private int speed;
    private int difficulty;
    // Start is called before the first frame update
    void Start()
    {
        // Use similar logic to virus from project 5
        // If within radius, then follow player
        // otherwise have some type of random roaming technique?
    }

    // Update is called once per frame
    void Update()
    {
        // Update visual of mummy for each frame - if we want it to very based on position


        // Check if within radius of player
            // If true, move in that direction at given speed
        // else
            // Have it roam around pyramid/hallway
            // make it weakly travel towards player?

        // Difficulty - as player unlocks more scrolls, want mummy to become more difficult
        // So use this in logic somewhere

        
    }
}
