using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
   [SerializeField] float movementSpeed = 5.01f;
   [SerializeField] float jumpForce = 11.1f;
    [SerializeField] float mouseSensitivity = 450;
    float xRotation = 0f;
    [SerializeField] Transform playerCamera; // Assign your camera in the inspector

   [SerializeField] Transform groundCheck;
   [SerializeField] LayerMask ground;

    [SerializeField] AudioSource jumpSound;
    [SerializeField] Text coinsText;

    [SerializeField] AudioSource newMusic;

    [SerializeField] bool newMusicPlaying;



    Animator myAnim; // Sets up a reference to an animator object
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor in place

        myAnim = GetComponentInChildren<Animator>(); // Searches through the children of the current gameObject for a component of the appropriate type, then assigns it to the 'myAnim' reference

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move in the direction the player is facing
        Vector3 moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;
        rb.velocity = new Vector3(moveDirection.x * movementSpeed, rb.velocity.y, moveDirection.z * movementSpeed);

      myAnim.SetFloat("speed", moveDirection.magnitude); // Finds a parameter of type float with the given name in the animator that myAnim is pointing at and assigns the value contained within newVelocity.mangitude to it.

        // Handle mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // Horizontal
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // Vertical

        xRotation -= mouseY; // Minus the current mouse Y-axis movement to the current vertical rotation
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp vertical position, prevents it from flipping and limits the camera to 90 degrees in either direction

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Rotates camera up and down
        transform.Rotate(Vector3.up * mouseX); // Vertically rotates player left/right, turns player's body.


        myAnim.SetBool("isOnGround", groundCheck);



        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
            myAnim.SetTrigger("jumped");
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        jumpSound.Play();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(collision.transform.parent.gameObject);
            Jump();
        }

        if (collision.gameObject.CompareTag("musicChange") & newMusicPlaying == false)
        {
            newMusic.Play();
            newMusicPlaying = true;
        }

    }


    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 1f, ground);
    }
}
