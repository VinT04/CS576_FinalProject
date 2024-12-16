using UnityEngine;
using System.Collections;

public class IbisPuzzleGenerator : MonoBehaviour
{
    public GameObject ibisCanvas; // The canvas containing the jackal puzzle UI
    private IbisPuzzleManager puzzleManager;

    private void Start()
    {
        // Ensure the canvas is hidden at the start
        if (ibisCanvas != null)
        {
            ibisCanvas.SetActive(false);
        }
    }
    private void Awake()
    {
        // Find the puzzle manager within the canvas
        puzzleManager = ibisCanvas.GetComponentInChildren<IbisPuzzleManager>();
    }

    public void StartPuzzle()
    {
        if (ibisCanvas != null)
        {
            ibisCanvas.SetActive(true); // Show the canvas
            if (puzzleManager != null)
            {
                //puzzleManager.GeneratePuzzle(); // Initialize the puzzle
            }
            else
            {
                Debug.LogError("IbisPuzzleManager not found in the canvas!");
            }
        }
        else
        {
            Debug.LogError("IbisCanvas is not assigned in the Inspector!");
        }
    }

    public void CompletePuzzle()
    {
        StartCoroutine(CompPuzzle());
    }

    public IEnumerator CompPuzzle()
    {
        yield return new WaitForSeconds(3f);

        if (ibisCanvas != null)
        {
            ibisCanvas.SetActive(false); // Hide the canvas
        }

        int keysCollected = PlayerPrefs.GetInt("KeysCollected", 0);
        keysCollected++;
        PlayerPrefs.SetInt("KeysCollected", keysCollected);
        Debug.Log("Ibis completed");
        InteractionTextManager.Instance.ShowText("Ibis is satisfied. You may continue...");
        yield return new WaitForSeconds(3f);
        InteractionTextManager.Instance.ShowText("");
        InteractionTextManager.Instance.HideText();
    }
}