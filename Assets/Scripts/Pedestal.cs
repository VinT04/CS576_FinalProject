using UnityEngine;

public class Pedestal : MonoBehaviour
{
    private bool isPlayerNear = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            InteractionTextManager.Instance.ShowText("Press 'E' to start the minigame");
        }
        else{
            isPlayerNear = true;
            InteractionTextManager.Instance.ShowText("Press 'E' to start the minigame");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            InteractionTextManager.Instance.HideText();
        }
        else {
            isPlayerNear = false;
            InteractionTextManager.Instance.HideText();
        }
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            StartMinigame();
        }
    }

    private void StartMinigame()
    {
        InteractionTextManager.Instance.HideText();

        // Fetch the current room index from PlayerPrefs
        int roomIndex = PlayerPrefs.GetInt("RoomIndex", 0);

        // Use MinigameManager to start the appropriate minigame
        MinigameManager minigameManager = FindObjectOfType<MinigameManager>();
        if (minigameManager != null)
        {
            minigameManager.StartMinigame(roomIndex);
        }
        else
        {
            Debug.LogError("MinigameManager not found!");
        }
    }
}
