using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlakeMovement:MonoBehaviour
{
    private Animator animation_controller;
    private CharacterController character_controller;
    private CinemachineFreeLook camera;
    public Vector3 movement_direction;
    public float walking_velocity;
    public float velocity;
    private bool isDeath;
    private string[] jump_states;

    // Start is called before the first frame update
    void Start()
    {
        animation_controller = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
        camera = GameObject.Find("FreeLook Camera").GetComponent<CinemachineFreeLook>();
        movement_direction = new Vector3(0.0f, 0.0f, 0.0f);
        walking_velocity = 1.5f;
        velocity = 0.0f;
        isDeath = false;
        jump_states = new string[] { "jump over", "jump ascending", "jump mid air", "jump descending", "jump landing" };
    }

    // Update is called once per frame
    void Update()
    {
        if (this.jump_states.Any(element => animation_controller.GetCurrentAnimatorStateInfo(0).IsName(element)))
        {
            velocity = Mathf.MoveTowards(velocity, walking_velocity * 3.0f, Time.deltaTime);
        }
        else if (isDeath)
        {
            velocity = 0f;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    // Jump
                    velocity = Mathf.MoveTowards(velocity, walking_velocity * 3.0f, Time.deltaTime);
                    animation_controller.Play("jump start");
                }
                else
                {
                    // Sprint forwards
                    velocity = Mathf.MoveTowards(velocity, walking_velocity * 2.0f, Time.deltaTime);
                    animation_controller.SetInteger("state", 2);
                }
            }
            else
            {
                // Walk forwards
                velocity = Mathf.MoveTowards(velocity, walking_velocity, Time.deltaTime);
                animation_controller.SetInteger("state", 1);
            }
        }
        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            velocity = Mathf.MoveTowards(velocity, 0, Time.deltaTime);
            animation_controller.SetInteger("state", 0);
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Transform cameraTransform = this.camera.transform;
        float xdirection = Mathf.Sin(Mathf.Deg2Rad * cameraTransform.rotation.eulerAngles.y);
        float zdirection = Mathf.Cos(Mathf.Deg2Rad * cameraTransform.rotation.eulerAngles.y);
        movement_direction = new Vector3(xdirection, 0.0f, zdirection);

        // character controller's move function is useful to prevent the character passing through the terrain
        // (changing transform's position does not make these checks)
        character_controller.Move(movement_direction * velocity * Time.deltaTime);

    }

}

