using UnityEngine;
using System.Collections;

public class PlayerController: MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        GameObject.Find("/Player/Body").GetComponent<HPcounter>().typeOb = 1;
    }

    float speed = 6.0f;
    float jumpSpeed = 8.0f;
    float gravity = 20.0f;
    float rotSpeed = 900;

    private Vector3 moveDirection = Vector3.zero;

    void Play()
    {
        gameController.Start2();
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

    void Update()
    {
        gameController.Update2();
        if (!gameController.paused)
            Play();
    }
}

