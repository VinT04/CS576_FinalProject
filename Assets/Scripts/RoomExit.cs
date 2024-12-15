using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomExit : MonoBehaviour
{
    private bool isPlayerNear = false;
    private GameObject player;

    public string mazeSceneName = "Intro_maze";

    // Dictionary to map room indices to exit locations
    private static readonly Dictionary<int, Vector3> roomExitLocations = new Dictionary<int, Vector3>
    {
        { 1, new Vector3(10f, 0f, 15f) }, // Replace with actual exit positions
        { 2, new Vector3(20f, 0f, 5f) },
        { 3, new Vector3(5f, 0f, 25f) },
        { 4, new Vector3(15f, 0f, 20f) },
        { 5, new Vector3(30f, 0f, 10f) }
    };

    private void OnTriggerEnter(Collider other)
    {
        isPlayerNear = true;
        player = other.gameObject;
        Debug.Log("ran");
        InteractionTextManager.Instance?.ShowText($"Press 'F' to exit room");
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerNear = false;
        player = null;
        InteractionTextManager.Instance?.HideText();
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Exiting the room...");
            StartCoroutine(ExitRoom());
        }
    }

    private IEnumerator ExitRoom()
    {
        // Retrieve the room index
        int roomIndex = PlayerPrefs.GetInt("RoomIndex", 1);
        Debug.Log($"Leaving Room {roomIndex}");
        // Load the maze scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mazeSceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Get the corresponding exit location
        Vector3 exitLocation = roomExitLocations.ContainsKey(roomIndex)
            ? roomExitLocations[roomIndex]
            : new Vector3(0f, 0f, 0f); // Default fallback location

        // Move the player to the exit location
        if (player != null)
        {
            //player.GetComponent<BlakeMovement>().teleport(exitLocation);
        }
    }
}
