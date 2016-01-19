using UnityEngine;
using System.Collections;

public class GoodControll : MonoBehaviour {

    private GameObject Game;

    // Use this for initialization
    void Start()
    {
        Game = GameObject.Find("/Game");
        GameObject.Find("/Player").GetComponent<HPcounter>().typeOb = 1;
    }

    float speed = 6.0f;
    float jumpSpeed = 8.0f;
    float gravity = 20.0f;
    float rotSpeed = 900;

    private Vector3 moveDirection = Vector3.zero;

    void Detect()
    {

    }

    float CountX()
    {
        return 0;
    }

    float CountRotX()
    {
        return 0;
    }

    float CountZ()
    {
        return 0;
    }

    void Play()
    {
        Detect();

        transform.Rotate(0, CountRotX() * rotSpeed * Time.deltaTime, 0);

        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            moveDirection = new Vector3(CountX(), 0,
                                    CountZ());
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
        if (!Game.GetComponent<gameController>().paused)
            Play();
    }
}
