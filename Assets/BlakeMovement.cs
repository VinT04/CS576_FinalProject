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
                    moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
                    animation_controller.SetInteger("state", 1);
                }
            }
            else
            {
                // Stop moving
                moveDirection = transform.forward * Input.GetAxis("Vertical") * (speed * 5f);
                animation_controller.SetInteger("state", 0);
            }
        }

        // Handle the rotation of the character
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Camera.transform.RotateAround(transform.position, Vector3.up, -100f * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Camera.transform.RotateAround(transform.position, Vector3.up, 100f * Time.deltaTime);
        }

        // Handle movement of character and camera follow
        character_controller.Move(moveDirection * Time.deltaTime);
        Camera.transform.position = Camera.transform.position + moveDirection * Time.deltaTime;
        if (transform.position.y > 0f)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        if (transform.position.y < 0f || Camera.transform.position.y < 1.68f)
        {
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            Camera.transform.position = new Vector3(Camera.transform.position.x, 1.68f, Camera.transform.position.z);
        }
    }
}


