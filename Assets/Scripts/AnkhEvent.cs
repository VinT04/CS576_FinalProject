using UnityEngine;
using System.Collections;

public class AnkhEvent : MonoBehaviour
{
    public GameObject ankhPrefab; // The Ankh object to show
    public AudioClip ankhSound; // Sound effect for the Ankh
    private AudioSource audioSource;

    private void Start()
    {
        // Initialize the audio source
        audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator ReceiveAnkh()
    {
        // Step 1: Destroy all existing Ankh objects in the scene
        GameObject[] existingAnkhs = GameObject.FindGameObjectsWithTag("Ankh"); // Ensure Ankh objects have the "Ankh" tag
        foreach (GameObject existingAnkh in existingAnkhs)
        {
            Destroy(existingAnkh);
        }

        yield return null; // Wait for the frame to ensure all Ankhs are destroyed

        // Step 2: Find the player in the scene and spawn the Ankh
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player object not found! Ensure the player has the correct tag.");
            yield break;
        }

        Vector3 playerPosition = player.transform.position;

        GameObject ankh = Instantiate(ankhPrefab, playerPosition + new Vector3(0, 0.5f, 0.5f), Quaternion.identity);
        ankh.tag = "Ankh"; // Assign the tag to the new Ankh
        ankh.transform.localScale = Vector3.zero; // Start small for animation

        // Step 3: Animate the Ankh scaling up
        float elapsedTime = 0f;
        float scaleDuration = 4f;
        Vector3 targetScale = new Vector3(0.5f,0.5f,0.5f);
        float rotationSpeed = 180f; // Rotation speed in degrees per second

        // Step 4: Play sound effect
        if (ankhSound != null)
        {
            audioSource.PlayOneShot(ankhSound);
        }

        while (elapsedTime < scaleDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / scaleDuration);

            // Update scale
            ankh.transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, progress);

            // Update rotation
            ankh.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            yield return null;
        }

        // Step 5: Show a notification or message
        InteractionTextManager.Instance.ShowText("You have received the Ankh");

        // Step 6: Wait for the display duration
        yield return new WaitForSeconds(2.0f);

        // Step 7: Hide notification and destroy the Ankh
        InteractionTextManager.Instance.HideText();
        PlayerPrefs.SetInt("GameWon", 1); // Mark the game as won
        Destroy(ankh);
    }


}
