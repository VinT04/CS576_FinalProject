using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using System.IO;

public class BlakeMovement:MonoBehaviour
{
    public bool living = true;
    public GameObject portal;
    public float gravity = 25.0f;
    public bool isRotatingLeft = false;
    public bool isRotatingRight = false;
    public Vector3 movement_direction;
    public GameObject gameWonCanvas;

    private Animator animation_controller;
    private CharacterController character_controller;
    private Vector3 moveDirection = Vector3.zero;
    private string die_anim;
    private bool gameWon = false;


    // Start is called before the first frame update
    void Start()
    {
        animation_controller = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PortalSeven" || other.gameObject.name == "Cylinder.013" || other.gameObject.name == "Cylinder.012")
        {
            string filename = Application.dataPath + "/scores.txt";
            string filename2 = Application.dataPath + "/visited.txt";

            File.WriteAllText(filename, string.Empty);
            File.WriteAllText(filename2, string.Empty);

            SceneManager.LoadScene("intro_maze");
        }

        if (PlayerPrefs.GetInt("GameWon", 0) == 1 && (other.gameObject.name == "PortalSeven - End" || other.gameObject.name == "Cylinder.013 - End" || other.gameObject.name == "Cylinder.012 - End"))
        {
            gameWon = true;
            gameWonCanvas.SetActive(true);

        }
    }

    void Update()
    {
        if (animation_controller.GetInteger("state") != -1 && !gameWon)
        {
            // End if dead
            if (!living)
            {
                animation_controller.SetInteger("state", -1);
                animation_controller.Play("dead2");
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
                    moveDirection = transform.forward * Input.GetAxis("Vertical") * 2f;
                    if (Input.GetKey(KeyCode.Space))
                    {
                        // Forward jump
                        moveDirection = transform.forward * Input.GetAxis("Vertical") * 3f;
                        moveDirection.y += 7f;
                        animation_controller.Play("jump start");
                    }
                    else if (Input.GetKey(KeyCode.LeftShift))
                    {
                        // Sprint
                        moveDirection = transform.forward * Input.GetAxis("Vertical") * 5f;
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
}


