using UnityEngine;
using System.Collections;

public class CowPuzzleGenerator : MonoBehaviour
{
    public GameObject hathorCanvas; // Reference to the HathorCanvas object

    private void Start()
    {
        // Ensure the canvas is hidden at the start
        if (hathorCanvas != null)
        {
            hathorCanvas.SetActive(false);
        }
    }

    public void StartPuzzle()
    {
        if (hathorCanvas != null)
        {
            hathorCanvas.SetActive(true); // Show the canvas
        }
    }

    public void CompletePuzzle()
    {
        StartCoroutine(CompPuzzle());
    }

    public IEnumerator CompPuzzle()
    {
        if (hathorCanvas != null)
        {
            hathorCanvas.SetActive(false); // Hide the canvas
        }

        int keysCollected = PlayerPrefs.GetInt("KeysCollected", 0);
        keysCollected++;
        PlayerPrefs.SetInt("KeysCollected", keysCollected);
        Debug.Log("Hathor completed");
        InteractionTextManager.Instance.ShowText("Hathor is pleased. You may proceed...");
        yield return new WaitForSeconds(3f);
        InteractionTextManager.Instance.ShowText("");
        InteractionTextManager.Instance.HideText();
    }
}
