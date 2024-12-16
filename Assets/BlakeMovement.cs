using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class BlakeMovement:MonoBehaviour
{
    public bool living = true;
    public float velocity_forward = 0f;
    public GameObject FollowTarget;
    public Vector3 movement_direction;
    private Animator animation_controller;
    private CharacterController character_controller;
    private Vector3 moveDirection = Vector3.zero;
    public float jumpTarget = 10f;
    public float gravity = 25.0f;
    public bool isRotatingLeft = false;
    public bool isRotatingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        animation_controller = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // End if dead
        if (!living)
        {
            int randomIndex = Random.Range(0, 3); // Generate a random number between 0 and 2
            switch (randomIndex)
            {
                case 0:
                    animation_controller.Play("crashing");
                    break;
                case 1:
                    animation_controller.Play("dead");
                    break;
                case 2:
                    animation_controller.Play("dead2");
                    break;
            }
        }
        if (!animation_controller.GetCurrentAnimatorStateInfo(0).IsName("jump start") &&
            !animation_controller.GetCurrentAnimatorStateInfo(0).IsName("jump ascending") &&
            !animation_controller.GetCurrentAnimatorStateInfo(0).IsName("jump mid air") &&
            !animation_controller.GetCurrentAnimatorStateInfo(0).IsName("jump descending") &&
            !animation_controller.GetCurrentAnimatorStateInfo(0).IsName("jump landing"))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                // Jump
                moveDirection.y += 10f;
                animation_controller.Play("jump start");
            }
            else if (Input.GetKey(KeyCode.W))
            {
                // Walk
                moveDirection = transform.forward * Input.GetAxis("Vertical")*2f;
                if (Input.GetKey(KeyCode.Space))
                {
                    // Forward jump
                    moveDirection.y += 10f;
                    animation_controller.Play("jump start");
                }
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    // Sprint
                    moveDirection = transform.forward * Input.GetAxis("Vertical") * 10f;
                    if (Input.GetKey(KeyCode.Space))
                    {
                        // Fast forward jump
                        moveDirection.y += 10f;
                        animation_controller.Play("jump start");
                    }
                    else
                    {
                        animation_controller.SetInteger("state", 2);
                    }
                }
                else if (Input.GetKey(KeyCode.Space))
                {
                    moveDirection.y += 10f;
                    animation_controller.Play("jump start");
                }
                else
                {
                    // Walk
                    animation_controller.SetInteger("state", 1);
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                // Walk backwards
                moveDirection = transform.forward * Input.GetAxis("Vertical");
                animation_controller.SetInteger("state", 6);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                // Walk left
                moveDirection = transform.right * Input.GetAxis("Horizontal");
                animation_controller.SetInteger("state", 5);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                // Walk right
                moveDirection = transform.right * Input.GetAxis("Horizontal");
                animation_controller.SetInteger("state", 4);
            }
            else
            {
                moveDirection = Vector3.zero;
                animation_controller.SetInteger("state", 0);
            }
        }

        // Handle the rotation of the character
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isRotatingLeft = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            isRotatingLeft = false;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isRotatingRight = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            isRotatingRight = false;
        }
        if (isRotatingLeft)
        {
            transform.Rotate(0, -100f * Time.deltaTime, 0);
        }
        if (isRotatingRight)
        {
            transform.Rotate(0, 100f * Time.deltaTime, 0);
        }

        // Handle movement of character and camera follow
        character_controller.Move(moveDirection * Time.deltaTime);
        if (transform.position.y > -1.95f)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        if (transform.position.y < -1.95f)
        {
            transform.position = new Vector3(transform.position.x, -1.95f, transform.position.z);
        }
    }
}


