using UnityEngine;
using System.Collections;

public class PlayerController: MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    float speed = 6.0f;
    float jumpSpeed = 8.0f;
    float gravity = 20.0f;
    float rotSpeed = 900;

    private Vector3 moveDirection = Vector3.zero;

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime, 0);

        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0,
                                    Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }
}
