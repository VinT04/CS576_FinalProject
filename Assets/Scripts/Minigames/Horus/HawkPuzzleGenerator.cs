using UnityEngine;

public class HawkPuzzleGenerator : MonoBehaviour
{
    public GameObject lightSourcePrefab;
    public GameObject targetPrefab;

    public Vector3 lightSourcePosition = new Vector3(-0.75f, 2, -2); // Fixed float division issue
    public Vector3 targetPosition = new Vector3(0, 2, -2);

    public AudioClip lightSourceAudioClip; // Drag your audio clip here in the Inspector

    public void GeneratePuzzle(Transform parent)
    {
        Debug.Log("Generating Hawk Puzzle...");

        // Instantiate light source
        GameObject lightSource = Instantiate(lightSourcePrefab, lightSourcePosition, Quaternion.identity, parent);
        lightSource.name = "LightSource";

        // Attach LightSourceController script to the light source
        LightSourceController lightController = lightSource.AddComponent<LightSourceController>();
        lightController.rotationSpeed = 100f; // Set rotation speed if needed
        lightController.rotationAxis = Vector3.up; // Set the rotation axis

        // Optionally change the position of the light source
        lightSource.transform.position = new Vector3(0f, 2f, 1.5f);
        Debug.Log($"LightSource new position: {lightSource.transform.position}");

        // Add and configure AudioSource
        AudioSource audioSource = lightSource.AddComponent<AudioSource>();
        if (lightSourceAudioClip != null)
        {
            audioSource.clip = lightSourceAudioClip; // Assign the audio clip
            audioSource.playOnAwake = false;        // Prevent audio from playing automatically
            audioSource.loop = true;               // Set loop if the sound should repeat
            audioSource.spatialBlend = 1f;         // Set to 1 for 3D sound (0 for 2D)
            audioSource.minDistance = 1f;          // Set minimum distance for 3D sound
            audioSource.maxDistance = 20f;         // Set maximum distance for 3D sound
        }
        else
        {
            Debug.LogError("No audio clip assigned to lightSourceAudioClip!");
        }

        // Instantiate target
        GameObject target = Instantiate(targetPrefab, targetPosition, Quaternion.identity, parent);
        target.name = "LightTarget";

        // Optionally change the position of the target
        target.transform.position = new Vector3(-5f, 2f, -4f);
        Debug.Log($"LightTarget new position: {target.transform.position}");
    }
}
