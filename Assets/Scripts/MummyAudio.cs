using UnityEngine;

public class MummyAudio : MonoBehaviour
{
    public AudioClip mummySound;          // The audio clip to play for the mummy
    public float detectionRange = 10f;    // Detection range for the player
    public AudioSource backgroundMusic;   // Reference to the background music AudioSource

    private AudioSource audioSource;      // AudioSource for the mummy's sound
    private GameObject player;            // Reference to the player
    private bool isPlaying = false;       // Track whether mummy's sound is playing

    void Start()
    {
        // Get or add the AudioSource component for the mummy
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configure the mummy's AudioSource
        audioSource.clip = mummySound;
        audioSource.loop = true;          // Loop the mummy sound
        audioSource.playOnAwake = false;  // Do not play on awake
        audioSource.spatialBlend = 1.0f;  // 3D sound

        // Find the player GameObject
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player object not found! Ensure the player has the 'Player' tag.");
        }

        // Check if background music AudioSource is assigned
        if (backgroundMusic == null)
        {
            Debug.LogWarning("Background music AudioSource not assigned!");
        }
    }

    void Update()
    {
        if (player == null || backgroundMusic == null) return;

        // Calculate distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            Debug.Log("mummy theme should play");
            if (!isPlaying)
            {
                // Pause background music and play the mummy sound
                backgroundMusic.volume = Mathf.MoveTowards(backgroundMusic.volume, 25.0f, 5.0f * Time.deltaTime);
                audioSource.Play();
                isPlaying = true;
            }
        }
        else
        {
            if (isPlaying)
            {
                // Stop mummy sound and resume background music
                audioSource.Stop();
                backgroundMusic.volume = Mathf.MoveTowards(backgroundMusic.volume, 50.0f, 5.0f * Time.deltaTime);
                isPlaying = false;
            }
        }
    }
}
