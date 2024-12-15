using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CowPuzzleManager : MonoBehaviour
{
    public Button[] buttons; // Buttons for the notes
    public AudioClip[] noteClips; // Clips for the notes
    public AudioClip successClip; // Clip for success
    public AudioClip failureClip; // Clip for failure

    private List<int> targetSequence; // Correct sequence of notes
    private List<int> playerSequence; // Player's input
    private AudioSource audioSource;

    private void Start()
    {
        // Initialize player sequence
        playerSequence = new List<int>();

        // Assign click events to buttons
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Capture index for closure
            buttons[i].onClick.AddListener(() => PlayNoteAndCheck(index));
        }

        // Generate the target sequence
        GenerateSequence();

        // Initialize AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    private void GenerateSequence()
    {
        targetSequence = new List<int>();
        for (int i = 0; i < buttons.Length; i++)
        {
            targetSequence.Add(Random.Range(0, noteClips.Length)); // Random sequence
        }
        Debug.Log("Target Sequence: " + string.Join(", ", targetSequence));
    }

    private void PlayNoteAndCheck(int noteIndex)
    {
        // Play the note sound
        if (noteClips != null && noteIndex < noteClips.Length)
        {
            audioSource.volume = Mathf.Clamp(1f, 0f, 1f); // Ensure volume stays between 0 and 1
            audioSource.PlayOneShot(noteClips[noteIndex]);
        }

        // Add the note to the player's sequence
        playerSequence.Add(noteIndex);

        // Check if the player's input matches the target sequence so far
        for (int i = 0; i < playerSequence.Count; i++)
        {
            if (playerSequence[i] != targetSequence[i])
            {
                Debug.Log("Incorrect sequence! Try again.");
                audioSource.volume = Mathf.Clamp(0.2f, 0f, 1f); // Ensure volume stays between 0 and 1
                audioSource.PlayOneShot(failureClip);
                playerSequence.Clear(); // Reset sequence
                return;
            }
        }

        // Check if the player completed the sequence
        if (playerSequence.Count == targetSequence.Count)
        {
            Debug.Log("Puzzle solved!");
            CompletePuzzle();
        }
    }

    private void CompletePuzzle()
    {
        // Play success sound
        audioSource.PlayOneShot(successClip);

        // Notify the puzzle generator to complete
        CowPuzzleGenerator puzzleGenerator = FindObjectOfType<CowPuzzleGenerator>();
        if (puzzleGenerator != null)
        {
            puzzleGenerator.CompletePuzzle();
        }
    }
}
