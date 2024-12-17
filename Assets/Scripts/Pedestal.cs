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
        else
        {
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
        else
        {
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

        if (roomIndex == 2) // Check if we're in the Cow Room
        {
            // Find the Plane object and get its CowPuzzleGenerator component
            CowPuzzleGenerator puzzleGenerator = FindObjectOfType<CowPuzzleGenerator>();

            if (puzzleGenerator != null)
            {
                puzzleGenerator.StartPuzzle();
            }
            else
            {
                Debug.LogError("CowPuzzleGenerator not found! Ensure it's attached to the correct object.");
            }
        }
        else if (roomIndex == 3) // Check if we're in the Ibis Room
        {
            // Find the Plane object and get its CowPuzzleGenerator component
            IbisPuzzleGenerator puzzleGenerator = FindObjectOfType<IbisPuzzleGenerator>();

            if (puzzleGenerator != null)
            {
                puzzleGenerator.StartPuzzle();
            }
            else
            {
                Debug.LogError("IbisPuzzleGenerator not found! Ensure it's attached to the correct object.");
            }
        }
        else if (roomIndex == 4) // Check if we're in the Crocodile Room
        {
            // Find the Plane object and get its CowPuzzleGenerator component
            JackalPuzzleGenerator puzzleGenerator = FindObjectOfType<JackalPuzzleGenerator>();

            if (puzzleGenerator != null)
            {
                puzzleGenerator.StartPuzzle();
            }
            else
            {
                Debug.LogError("JackalPuzzleGenerator not found! Ensure it's attached to the correct object.");
            }
        }
        else if (roomIndex == 5) // Check if we're in the Crocodile Room
        {
            // Find the Plane object and get its CowPuzzleGenerator component
            CrocodilePuzzleGenerator puzzleGenerator = FindObjectOfType<CrocodilePuzzleGenerator>();

            if (puzzleGenerator != null)
            {
                puzzleGenerator.StartPuzzle();
            }
            else
            {
                Debug.LogError("CrocodilePuzzleGenerator not found! Ensure it's attached to the correct object.");
            }
        }
        else if (roomIndex == 6)
        {
            AnkhEvent ankhEvent = FindObjectOfType<AnkhEvent>();
        
            if (ankhEvent != null)
            {
                // Start the coroutine from the other script
                StartCoroutine(ankhEvent.ReceiveAnkh());
            }
            else
            {
                Debug.LogError("AnkhEvent component not found in the scene!");
            }
        }
    }

}