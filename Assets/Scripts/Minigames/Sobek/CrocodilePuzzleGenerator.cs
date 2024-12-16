using UnityEngine;
using System.Collections;

public class CrocodilePuzzleGenerator : MonoBehaviour
{
    public GameObject crocodileCanvas; // The canvas containing the crocodile puzzle UI
    private CrocodilePuzzleManager puzzleManager;

    private void Start()
    {
        // Ensure the canvas is hidden at the start
        if (crocodileCanvas != null)
        {
            crocodileCanvas.SetActive(false);
        }
    }
    private void Awake()
    {
        // Find the puzzle manager within the canvas
        puzzleManager = crocodileCanvas.GetComponentInChildren<CrocodilePuzzleManager>();
    }

    public void StartPuzzle()
    {
        if (crocodileCanvas != null)
        {
            crocodileCanvas.SetActive(true); // Show the canvas
            if (puzzleManager != null)
            {
                //puzzleManager.GeneratePuzzle(); // Initialize the puzzle
            }
            else
            {
                Debug.LogError("CrocodilePuzzleManager not found in the canvas!");
            }
        }
        else
        {
            Debug.LogError("CrocodileCanvas is not assigned in the Inspector!");
        }
    }

    public void CompletePuzzle()
    {
        StartCoroutine(CompPuzzle());
    }

    public IEnumerator CompPuzzle()
    {
        if (crocodileCanvas != null)
        {
            crocodileCanvas.SetActive(false); // Hide the canvas
        }

        int keysCollected = PlayerPrefs.GetInt("KeysCollected", 0);
        keysCollected++;
        PlayerPrefs.SetInt("KeysCollected", keysCollected);
        Debug.Log("Sobek completed");
        InteractionTextManager.Instance.ShowText("Sobek acknowledges you. You may proceed...");
        yield return new WaitForSeconds(3f);
        InteractionTextManager.Instance.ShowText("");
        InteractionTextManager.Instance.HideText();
    }
}