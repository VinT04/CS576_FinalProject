using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DoorInteraction : MonoBehaviour
{
    private bool isPlayerNear = false;
    private GameObject player;

    public string roomSceneName = "Room"; // Base room scene name
    public int roomIndex; // Room identifier (e.g., 1 to 5)
    public string mazeSceneName = "Intro_maze";
    public Text interactionText; // Reference to the TextMeshProUGUI element

    private void Start()
    {
        // Locate the InteractionText UI element dynamically
        GameObject textObject = GameObject.Find("InteractionText");
        if (textObject != null)
        {
            interactionText = textObject.GetComponent<Text>();
            interactionText.gameObject.SetActive(false); // Ensure it's hidden initially
        }
        else
        {
            Debug.LogError("Interaction Text object not found in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            player = other.gameObject;
            Debug.Log($"Player is near door to Room {roomIndex}. Press 'F' to enter.");
        }

        // Display the interaction text
        if (interactionText != null)
        {
            interactionText.text = $"Press 'F' to enter room";
            interactionText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            player = null;
            Debug.Log("Player left the door area.");
        }
        // Hide the interaction text
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log($"Entering Room {roomIndex}...");
            StartCoroutine(EnterRoom());
        }
    }

    private IEnumerator EnterRoom()
    {
        // Save the player's current position for returning later
        Vector3 exitDoorPosition = transform.position;

        // Save the room index and exit door position to PlayerPrefs
        PlayerPrefs.SetInt("RoomIndex", roomIndex);
        PlayerPrefs.SetFloat("ExitDoorX", exitDoorPosition.x);
        PlayerPrefs.SetFloat("ExitDoorY", exitDoorPosition.y);
        PlayerPrefs.SetFloat("ExitDoorZ", exitDoorPosition.z);
        PlayerPrefs.Save();

        // Load the room scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(roomSceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move the player to the room's starting position
        if (player != null)
        {
            //player.GetComponent<BlakeMovement>().teleport(new Vector3(2f, 1f, 2f)); // Adjust room start position
        }
    }
}
