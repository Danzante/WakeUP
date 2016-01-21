using UnityEngine;
using System.Collections;
using System;

public class GoodControll : MonoBehaviour {

    private GameObject Game;

    // Use this for initialization
    void Start()
    {
        Game = GameObject.Find("/Game");
        GameObject.Find("/Player").GetComponent<HPcounter>().typeOb = 1;
        aimX = transform.position.x;
        aimZ = transform.position.z;
        aimRot = transform.rotation.y;
    }

    System.Random r = new System.Random();

    float speed = 6.0f;
    float jumpSpeed = 8.0f;
    float gravity = 20.0f;
    float rotSpeed = 900;

    private Vector3 moveDirection = Vector3.zero;

    float aimX, aimZ, aimRot;

    struct cord  {
        public float x, z, rot;
    }

    Stack stack = new Stack();
    int memory = 0;

    void Detect()
    {
        memory += 1;
        cord a;
        a.x = aimX;
        a.z = aimZ;
        a.rot = aimRot;
        stack.Push(a);
        for(int i = 4; i < memory; i++)
        {
            if(r.Next(0, 6) < 2)
            {
                memory = i;
            }
        }
    }

    float CountX()
    {
        if(aimX == transform.position.x && aimZ == transform.position.z)
        {
            Detect();
        }
        return aimX - transform.position.x;
    }

    float CountRotX()
    {
        if (aimX == transform.position.x && aimZ == transform.position.z)
        {
            Detect();
        }
        return aimRot - transform.rotation.y;
    }

    float CountZ()
    {
        if (aimX == transform.position.x && aimZ == transform.position.z)
        {
            Detect();
        }
        return aimZ - transform.position.Z;
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
