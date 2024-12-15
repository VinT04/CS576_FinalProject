using UnityEngine;

public class PipeLogic : MonoBehaviour
{
    public enum PipeType
    {
        Straight,
        Elbow
    }

    public PipeType pipeType; // Set this in the inspector or dynamically during initialization
    public int CurrentOrientation { get; private set; } // Current orientation (0, 1 for straight; 0, 1, 2, 3 for elbow)

    private CrocodilePuzzleManager puzzleManager;

    private void Start()
    {
        puzzleManager = FindObjectOfType<CrocodilePuzzleManager>();
    }

    private void OnMouseDown()
    {
        RotatePipe();
    }

    public void RotatePipe()
    {
        // Rotate pipe based on its type
        if (pipeType == PipeType.Straight)
        {
            // Straight pipes have only two orientations: 0 and 1 (horizontal and vertical)
            CurrentOrientation = (CurrentOrientation + 1) % 2;
            transform.rotation = Quaternion.Euler(0, 0, CurrentOrientation * 90);
        }
        else if (pipeType == PipeType.Elbow)
        {
            // Elbow pipes have four orientations: 0, 1, 2, 3
            CurrentOrientation = (CurrentOrientation + 1) % 4;
            transform.rotation = Quaternion.Euler(0, 0, CurrentOrientation * 90);
        }

        Debug.Log(CurrentOrientation);
        // Notify the puzzle manager to check the puzzle state
        puzzleManager?.CheckPuzzleCompletion();
    }
}
