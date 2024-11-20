using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BlakeMovement:MonoBehaviour
{

    public Transform FollowTarget;
    public Quaternion targetRotation;
    public float velocity = 0f;
    public Vector3 movement_direction;
    private Animator animation_controller;
    private CharacterController character_controller;
    public float jumpforce = 10f;
    public Vector3 jumpTarget;

    // Start is called before the first frame update
    void Start()
    {
        FollowTarget = GameObject.Find("Follow Target").GetComponent<Transform>();
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
            transform.position = Vector3.MoveTowards(transform.position, jumpTarget, Time.deltaTime);
            if (Vector3.Distance(transform.position, jumpTarget) < 0.1f)
            {
                jumpTarget = transform.position - new Vector3(0, 1.5f, 0);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        Debug.Log("JUMP");
                        // Jump
                        velocity = Mathf.MoveTowards(velocity, 5.0f, Time.deltaTime);
                        Vector3 jumpTarget = transform.position + new Vector3(0, 1.5f, 0);
                        jumpTarget = transform.position + new Vector3(0, 1.5f, 0);
                        animation_controller.Play("jump start");
                    }
                    else
                    {
                        Debug.Log("RUNNING");
                        // Sprint forwards
                        velocity = Mathf.MoveTowards(velocity, 3.0f, Time.deltaTime * 2);
                        animation_controller.SetInteger("state", 2);
                    }
                }
                else
                {
                    // Walk forwards
                    Debug.Log("WALKING");
                    velocity = Mathf.MoveTowards(velocity, 1f, Time.deltaTime);
                    animation_controller.SetInteger("state", 1);
                }
                gameObject.transform.rotation = Quaternion.Euler(0, FollowTarget.eulerAngles.y + Input.GetAxis("Mouse X") * 1f, 0);
            }
            else
            {
                velocity = Mathf.MoveTowards(velocity, 0, Time.deltaTime * 2);
                animation_controller.SetInteger("state", 0);
            }
        }

        float mouseX = Input.GetAxis("Mouse X"); // Calculate the target rotation based on mouse input
        FollowTarget.rotation = Quaternion.Euler(0, FollowTarget.eulerAngles.y + mouseX * 1f, 0);

        float xdirection = Mathf.Sin(Mathf.Deg2Rad * FollowTarget.rotation.eulerAngles.y);
        float zdirection = Mathf.Cos(Mathf.Deg2Rad * FollowTarget.rotation.eulerAngles.y);
        movement_direction = new Vector3(xdirection, 0.0f, zdirection);

        // character controller's move function is useful to prevent the character passing through the terrain
        // (changing transform's position does not make these checks)
        character_controller.Move(movement_direction * velocity * Time.deltaTime);
    }

}

