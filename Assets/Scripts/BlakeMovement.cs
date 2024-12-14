using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BlakeMovement:MonoBehaviour
{
    public Camera Camera;
    public Quaternion targetRotation;
    public float velocity_forward = 0f;
    public Vector3 movement_direction;
    private Animator animation_controller;
    private CharacterController character_controller;
    private Vector3 moveDirection = Vector3.zero;
    public float jumpTarget = 10f;
    public float gravity = 25.0f;
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        animation_controller = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animation_controller.GetCurrentAnimatorStateInfo(0).IsName("jump start") ||
            animation_controller.GetCurrentAnimatorStateInfo(0).IsName("jump ascending") ||
            animation_controller.GetCurrentAnimatorStateInfo(0).IsName("jump mid air") ||
            animation_controller.GetCurrentAnimatorStateInfo(0).IsName("jump descending") ||
            animation_controller.GetCurrentAnimatorStateInfo(0).IsName("jump landing"))
        {
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.rotation = Camera.transform.rotation;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        // Jump
                        if (transform.position.y == 0f)
                        {
                            moveDirection.y += 10f;
                            animation_controller.Play("jump start");
                        }
                    }
                    else
                    {
                        // Sprint forwards
                        moveDirection = transform.forward * Input.GetAxis("Vertical") * (speed * 5f);
                        animation_controller.SetInteger("state", 2);
                    }
                }
                else
                {
                    
                    // Walk forwards
                    moveDirection = transform.forward * Input.GetAxis("Vertical") * speed*2f;
                    animation_controller.SetInteger("state", 1);
                }
            }
            else
            {
                // Stop moving
                moveDirection = transform.forward * Input.GetAxis("Vertical") * (speed * 0f);
                animation_controller.SetInteger("state", 0);
            }
            // Jump
            if (Input.GetKey(KeyCode.Space))
            {
                if (transform.position.y == 0f)
                {
                    moveDirection.y += 10f;
                    animation_controller.Play("jump start");
                }
            }
        }

        // Rotate Camera
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Camera.transform.RotateAround(transform.position, Vector3.up, -90f * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Camera.transform.RotateAround(transform.position, Vector3.up, 90f * Time.deltaTime);
        }

        // Move
        character_controller.Move(moveDirection * Time.deltaTime);

        // Camera offset from player
        Vector3 cameraOffset = new Vector3(0, 1.8f, -1.3f); // Adjust Y and Z for third-person position
        Vector3 targetCameraPosition = transform.position + transform.TransformDirection(cameraOffset);

        float smoothSpeed = 5f;
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, targetCameraPosition, smoothSpeed * Time.deltaTime);

        // Gravity handling
        if (!character_controller.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime; // Apply gravity
        }
        else
        {
            moveDirection.y = 0f; // Reset vertical velocity when grounded
        }

    }

}


