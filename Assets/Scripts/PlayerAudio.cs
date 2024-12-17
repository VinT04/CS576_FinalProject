using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudio : MonoBehaviour
{
    [Header("Footstep Sounds")]
    public AudioClip[] footstepClips;       // Array of footstep sounds
    public float footstepInterval = 0.0f;   // Time between footstep sounds
    public float movementThreshold = 0.1f;  // Minimum speed to play footsteps

    [Header("Action Sounds")]
    public AudioClip jumpClip;              // Sound effect for jumping
    public AudioClip deathClip;             // Sound effect for death

    private AudioSource audioSource;        // AudioSource for playing sounds
    private CharacterController controller; // Reference to CharacterController
    private float footstepTimer = 0f;       // Timer for footstep intervals
    private bool hasJumped = false;         // Tracks if jump SFX has played

    void Start()
    {
        // Get AudioSource and CharacterController
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();

        if (audioSource == null)
            Debug.LogError("AudioSource component missing!");

        if (controller == null)
            Debug.LogError("CharacterController component missing!");
    }

    void Update()
    {
        HandleFootsteps();
        HandleJump();
    }

    private void HandleFootsteps()
    {
        // Check if player is grounded and moving
        if (controller.isGrounded && controller.velocity.magnitude > movementThreshold)
        {
            footstepTimer += Time.deltaTime;

            if (footstepTimer >= footstepInterval)
            {
                PlayFootstep();
                footstepTimer = 0f; // Reset timer
            }
        }
        else
        {
            footstepTimer = 0f; // Reset if not moving
        }
    }

    private void HandleJump()
    {
        // Detect jump (e.g., upward velocity)
        if (!controller.isGrounded && !hasJumped && controller.velocity.y > 0.1f)
        {
            PlayJumpSFX();
            hasJumped = true;
        }

        // Reset jump state when player lands
        if (controller.isGrounded && hasJumped)
        {
            hasJumped = false;
        }
    }

    private void PlayFootstep()
    {
        if (footstepClips.Length > 0)
        {
            int index = Random.Range(0, footstepClips.Length);
            audioSource.PlayOneShot(footstepClips[index]);
        }
    }

    private void PlayJumpSFX()
    {
        if (jumpClip != null)
        {
            audioSource.PlayOneShot(jumpClip);
        }
    }

    public void PlayDeathSFX()
    {
        if (deathClip != null)
        {
            audioSource.PlayOneShot(deathClip);
        }
    }
}
