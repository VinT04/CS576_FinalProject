using UnityEngine;
using UnityEngine.UI;

public class IbisPuzzleManager : MonoBehaviour
{

    private int totalLeft = 0;
    private int totalRight = 0;
    private float rotationSpeed = 5f;

    private bool puzzleComplete = false;

    private Text status;

    private void Start()
    {
        // UpdateWeightTotals();
        status = GameObject.FindGameObjectWithTag("Status").GetComponent<Text>();
    }

    private void Update()
    {
        // UpdateScaleRotation();
        // CheckPuzzleCompletion();
    }

    public void updateStatus(string newStatus)
    {
        status.text = newStatus;
        if (newStatus == "Correct")
        {
            CompletePuzzle();
        }
    }

    private void CompletePuzzle()
    {
        Debug.Log("Puzzle completed!");

        IbisPuzzleGenerator generator = FindObjectOfType<IbisPuzzleGenerator>();
        if (generator != null)
        {
            generator.CompletePuzzle();
        }
    }

}
