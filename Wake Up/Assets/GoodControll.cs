﻿using UnityEngine;
using System.Collections;
using System;

public class GoodControll : MonoBehaviour {
    
    public int nowG = 17;
    const float epsilon = 0.1f;
    public bool p = true;

    public float aimX, aimZ, aimRot;
    public int ax;

    void Start()
    {
        ax = 1;
    }

    // Use this for initialization
    void Start2()
    {
        aimX = transform.position.x;
        aimZ = transform.position.z;
        aimRot = transform.rotation.y;
        ax = 0;
        for (int i = 0; i < gameController.gLen; i++)
        {
            if ((Mathf.Abs(aimX - gameController.gCordx[i]) < epsilon) && (Mathf.Abs(aimZ - gameController.gCordz[i]) < epsilon))
            {
                nowG = i;
                break;
            }
        }
    }

    private void escape()
    {
        //Destroy(gameObject);
        p = false;
    }

    System.Random r = new System.Random();

    float speed = 1.0f;
    float gravity = 20.0f;
    float rotSpeed = 900;

    private Vector3 moveDirection = Vector3.zero;

    int memory = 0;

    class St
    {
        int[] st = new int[100];
        int l = 0;

        public int Pop(int memory)
        {
            if (l > memory && memory > 0)
            {
                for (int i = 0; i < memory; i++)
                {
                    st[i] = st[l - memory + i];
                }
                l = memory;
            }
            if (l > 0)
            {
                l--;
                return st[l];
            }
            return 0;
        }

        public void Push(int a, int memory)
        {
            if(l > memory && memory > 0)
            {
                for(int i = 0; i < memory; i++)
                {
                    st[i] = st[l - memory + i];
                }
                l = memory;
            }
            st[l] = a;
            l++;
        }
    }

    St stack = new St();
    

    void Detect()
    {
        if(nowG == 0)
        {
            escape();
        }
        memory += 1;
        stack.Push(nowG, memory);
        if (memory > 4)
        {
            for (int i = 4; i < memory; i++)
            {
                if (r.Next(0, 7) < 2)
                {
                    memory = i;
                    break;
                }
            }
        }
        int l = gameController.gELen[nowG];
        int m;
        bool b;
        bool[] visit = new bool[l];
        if (memory > 0)
        {
            int[] a = new int[memory];
            for (int i = 0; i < memory; i++)
            {
                a[i] = stack.Pop(memory);
            }
            for (int i = memory - 1; i > -1; i--)
            {
                stack.Push(a[i], memory);
            }
            m = 0;
            for (int i = 0; i < l; i++)
            {
                b = true;
                for (int j = 0; j < memory; j++)
                {
                    if (a[j] == gameController.gEdge[nowG][i])
                    {
                        visit[i] = false;
                        b = false;
                        break;
                    }
                }
                if (b)
                {
                    visit[i] = true;
                    m++;
                }
            }
        }
        else
        {
            m = l;
            for (int i = 0; i < l; i++)
            {
                visit[i] = true;
            }
        }
        b = false;
        if(m == 0)
        {
            b = true;
            m = l;
        }
        int k = r.Next(m);
        for (int i = 0; i < l; i++)
        { 
            if (visit[i] || b)
            {
                if (k == 0)
                {
                    nowG = gameController.gEdge[nowG][i];
                    aimX = gameController.gCordx[nowG];
                    aimZ = gameController.gCordz[nowG];
                    return;
                }                
                k--;
            }
        }
    }

    float CountX()
    {
        return aimX - transform.position.x;
    }

    float CountRotX()
    {
        return aimRot - transform.rotation.y;
    }

    float CountZ()
    {
        return aimZ - transform.position.z;
    }

    void Play()
    {
        if (Mathf.Abs(aimX - transform.position.x) < epsilon && Mathf.Abs(aimZ - transform.position.z) < epsilon)
        {
            Detect();
        }

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
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }

    void Update()
    {
        if (ax == 1)
        {
            gameController.Start2();
            Start2();
        }
        gameController.Update2();
        if (!gameController.paused && p)
            Play();
    }
}
