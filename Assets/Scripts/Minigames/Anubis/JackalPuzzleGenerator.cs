using UnityEngine;
using System.Collections;

public class JackalPuzzleGenerator : MonoBehaviour
{
    public GameObject jackalCanvas; // The canvas containing the jackal puzzle UI
    private JackalPuzzleManager puzzleManager;

    private void Start()
    {
        // Ensure the canvas is hidden at the start
        if (jackalCanvas != null)
        {
            jackalCanvas.SetActive(false);
        }
    }
    private void Awake()
    {
        // Find the puzzle manager within the canvas
        puzzleManager = jackalCanvas.GetComponentInChildren<JackalPuzzleManager>();
    }

    public void StartPuzzle()
    {
        if (jackalCanvas != null)
        {
            jackalCanvas.SetActive(true); // Show the canvas
            if (puzzleManager != null)
            {
                //puzzleManager.GeneratePuzzle(); // Initialize the puzzle
            }
            else
            {
                Debug.LogError("JackalPuzzleManager not found in the canvas!");
            }
        }
        else
        {
            Debug.LogError("JackalCanvas is not assigned in the Inspector!");
        }
    }

    public void CompletePuzzle()
    {
        StartCoroutine(CompPuzzle());
    }

    public IEnumerator CompPuzzle()
    {
        yield return new WaitForSeconds(3f);

        if (jackalCanvas != null)
        {
            jackalCanvas.SetActive(false); // Hide the canvas
        }

        int keysCollected = PlayerPrefs.GetInt("KeysCollected", 0);
        keysCollected++;
        PlayerPrefs.SetInt("KeysCollected", keysCollected);
        Debug.Log("Anubis completed");
        InteractionTextManager.Instance.ShowText("Anubis has taken note. You may proceed...");
        yield return new WaitForSeconds(3f);
        InteractionTextManager.Instance.ShowText("");
        InteractionTextManager.Instance.HideText();
    }
}