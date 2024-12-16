using UnityEngine;
using System.Collections;

public class LightSourceController : MonoBehaviour
{
    public float rotationSpeed = 90f; // Speed of rotation
    public Vector3 rotationAxis = Vector3.up; // Default rotation axis (Y-axis)

    public float targetAngle = -140f; // Target angle for winning (e.g., 90 degrees)
    public float tolerance = 15f; // Allowable deviation from the target angle
    public float failThreshold = 2f; // Time in seconds to detect continued rotation after reaching the target

    public AudioClip rotationClip; // Assign this in the Inspector
    private AudioSource audioSource;

    private bool isRotating = false;
    private bool gameWon = false;
    private bool gameComplete = false; // Prevent multiple calls
    private float rotationStopTime;

    void Start()
    {
        // Add or get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configure the AudioSource
        audioSource.clip = rotationClip;
        audioSource.loop = true; // Loop the clip while rotating
        audioSource.playOnAwake = false; // Prevent audio from playing on start
    }

    void Update()
    {
        if (!gameWon) InteractionTextManager.Instance?.ShowText($"Press R to rotate pillar");
        if (Input.GetKey(KeyCode.R))
        {
            if (!isRotating)
            {
                StartRotationAudio(); // Start audio when rotation begins
            }

            RotateLightSource();
            isRotating = true;
        }
        else if (isRotating) // Detect when rotation stops
        {
            isRotating = false;
            rotationStopTime = Time.time; // Record the time when the player stopped rotating
            StopRotationAudio(); // Stop audio when rotation ends
        }

        CheckWinCondition();
    }

    private void RotateLightSource()
    {
        // Rotate the light source while 'R' is held
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }

    private void StartRotationAudio()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play(); // Play the audio
        }
    }

    private void StopRotationAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop(); // Stop the audio
        }
    }

    private void CheckWinCondition()
    {
        // Get the current angle around the Y-axis
        float currentAngle = transform.eulerAngles.y;

        // Normalize the angle to be between -180 and 180
        if (currentAngle > 180)
        {
            currentAngle -= 360;
        }

        // Check if the player stops within the target range
        if (!isRotating && !gameWon && Mathf.Abs(currentAngle - targetAngle) <= tolerance)
        {
            gameWon = true; // Mark the game as won
            rotationStopTime = Time.time; // Record when the player stopped
        }

        // Check if the player continues rotating after winning
        if (gameWon && isRotating)
        {
            ResetGame();
        }

        // Check if the player remains still for long enough after winning
        if (gameWon && !gameComplete && Time.time - rotationStopTime >= failThreshold)
        {
            StartCoroutine(CompleteGame());
        }
    }

    private void ResetGame()
    {
        gameWon = false;
        gameComplete = false;
        //Debug.Log("Game reset. Try again!");
    }

    private IEnumerator CompleteGame()
    {
        if (gameComplete) yield break; // Prevent multiple triggers
        gameComplete = true;

        rotationSpeed = 0f;

        // Add logic for winning the game (e.g., unlock a door, grant rewards, etc.)
        int keysCollected = PlayerPrefs.GetInt("KeysCollected", 0);
        keysCollected++;
        PlayerPrefs.SetInt("KeysCollected", keysCollected);
        Debug.Log("Horus completed");
        InteractionTextManager.Instance.ShowText("Horus has seen you. You may proceed...");
        yield return new WaitForSeconds(3f);
        InteractionTextManager.Instance.ShowText("");
        InteractionTextManager.Instance.HideText();
    }
}
