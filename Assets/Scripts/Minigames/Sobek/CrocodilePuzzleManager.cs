using UnityEngine;

public class CrocodilePuzzleManager : MonoBehaviour
{
    public PipeLogic[] pipes; // Flat array of all pipes (assign in the Inspector)
    public int[] correctOrientations; // The correct orientations for the puzzle
    public int gridSize = 5; // Size of one side of the grid (e.g., 5x5 = 25 pipes)

    private void Start()
    {
        InitializeCorrectOrientations();
        RandomizePipeRotations();
    }

    // Initializes the correct orientations for the puzzle.
    private void InitializeCorrectOrientations()
    {
        correctOrientations = new int[]
        {
            0, 0, 2, 3, 2,
            3, 2, 1, 1, 1,
            1, 1, 0, 1, 1,
            1, 1, 3, 2, 1,
            1, 0, 1, 0, 1
        };
    }

    // Randomizes the rotations of all pipes.
    private void RandomizePipeRotations()
    {
        for (int i = 0; i < pipes.Length; i++)
        {
            PipeLogic pipe = pipes[i];
            if (pipe == null) continue;

            int maxRotations = pipe.pipeType == PipeLogic.PipeType.Elbow ? 4 : 2; // Elbow pipes have 4 states, straight pipes have 2
            int randomRotations = Random.Range(0, maxRotations);

            for (int j = 0; j < randomRotations; j++)
            {
                pipe.RotatePipe();
            }
        }
    }

    // Checks if the puzzle is complete.
    public void CheckPuzzleCompletion()
    {
        for (int i = 0; i < pipes.Length; i++)
        {
            PipeLogic pipe = pipes[i];
            Debug.Log($"pipe {i} should be {correctOrientations[i]} and is {pipe.CurrentOrientation}");
            if (pipe == null || pipe.CurrentOrientation != correctOrientations[i])
            {
                return; // Puzzle is not solved yet
            }
        }

        Debug.Log("Puzzle solved!");
        CompletePuzzle();
    }

    // Called when the puzzle is completed.
    private void CompletePuzzle()
    {
        // Notify the puzzle generator to complete
        CrocodilePuzzleGenerator puzzleGenerator = FindObjectOfType<CrocodilePuzzleGenerator>();
        if (puzzleGenerator != null)
        {
            puzzleGenerator.CompletePuzzle();
        }
    }
}
