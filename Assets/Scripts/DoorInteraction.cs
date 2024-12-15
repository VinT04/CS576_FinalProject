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
    public Text interactionText; // text showing on bottom
    public int keysRequired = 5;


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
            //Debug.LogError("Interaction Text object not found in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered door collider");
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            player = other.gameObject;
            Debug.Log($"Player is near door to Room {roomIndex}. Press 'F' to enter.");
            
            if (roomIndex == 6) InteractionTextManager.Instance?.ShowText($"Press F to enter tomb");
            else InteractionTextManager.Instance?.ShowText($"Press F to enter room");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            player = null;
            InteractionTextManager.Instance?.HideText();
        }
    }


    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log($"Entering Room {roomIndex}...");

            if (roomIndex == 6)
            {
                int keysCollected = PlayerPrefs.GetInt("KeysCollected", 0);
                if (keysCollected >= keysRequired)
                {
                    StartCoroutine(EnterRoom());

                }
                else
                {
                    InteractionTextManager.Instance.ShowText($"You need {keysRequired - keysCollected} more keys to open this door.");
                }
            }
            else {
                StartCoroutine(EnterRoom());
            }
        }
    }

    private IEnumerator EnterRoom()
    {
        // Save the room index and exit door position to PlayerPrefs
        PlayerPrefs.SetInt("RoomIndex", roomIndex);
        PlayerPrefs.Save();

        // Load the room scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(roomSceneName);
        while (!asyncLoad.isDone)
        {
            InteractionTextManager.Instance?.HideText();
            yield return null;
        }

        // Move the player to the room's starting position
        if (player != null)
        {
            //player.GetComponent<BlakeMovement>().teleport(new Vector3(2f, 1f, 2f)); // Adjust room start position
        }
    }
}
