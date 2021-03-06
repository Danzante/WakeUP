﻿using UnityEngine;
using System.Collections;
using System;

public class GoodControll : MonoBehaviour {
    
    public int nowG = 17;
    const float epsilon = 0.1f;
    public bool p = true;

    public float aimX, aimZ;
    public int CurRot, aimRot;
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
        aimRot = 0;
        CurRot = 0;
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

    float speed = 3.0f;
    float gravity = 20.0f;

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
                    if(aimX - transform.position.x > epsilon)
                    {
                        aimRot = 90;
                    }
                    else if(aimX - transform.position.x < -epsilon)
                    {
                        aimRot = -90;
                    }
                    else if(aimZ - transform.position.z > epsilon)
                    {
                        aimRot = 0;
                    }
                    else
                    {
                        aimRot = 180;
                    }
                    return;
                }                
                k--;
            }
        }
    }


    float CountXNorth()
    {
        if (Mathf.Abs(aimZ - transform.position.z) < epsilon)
        {
            if (aimX < transform.position.x && aimRot == 0)
                return -1;
            else if (aimX > transform.position.x && aimRot == 0)
                return 1;
            else if (aimX < transform.position.x)
                return 1;
            return -1;
        }
        return 0;
    }

    float CountXWest()
    {
        if (Mathf.Abs(aimX - transform.position.x) < epsilon)
        {
            if (aimZ < transform.position.z && aimRot == 90)
                return 1;
            else if (aimZ > transform.position.z && aimRot == 90)
                return -1;
            else if (aimZ < transform.position.z)
                return -1;
            return 1;
        }
        return 0;
    }

    float CountX()
    {
        if(Mathf.Abs(aimRot) == 90)
            return CountXWest();
        return CountXNorth();
    }

    float CountRotX()
    {
        float res;
        if (CurRot == aimRot)
            res = 0;
        else if (CurRot - aimRot == 90 || (CurRot == -90 && aimRot == 180))
            res = -90;
        else if (aimRot - CurRot == 90 || (CurRot == 180 && aimRot == -90))
            res = 90;
        else
            res = 180;
        CurRot = aimRot;
        return res;
    }

    float CountZNorth()
    {
        if (Mathf.Abs(aimZ - transform.position.z) < epsilon)
            return 0;
        return 1;
    }

    float CountZWest()
    {
        if (Mathf.Abs(aimX - transform.position.x) < epsilon)
            return 0;
        return 1;
    }

    float CountZ()
    {
        if (Mathf.Abs(aimRot) == 90)
            return CountZWest();
        return CountZNorth();
    }

    void Play()
    {
        if (Mathf.Abs(aimX - transform.position.x) < epsilon && Mathf.Abs(aimZ - transform.position.z) < epsilon)
        {
            Detect();
        }

        transform.Rotate(0, CountRotX(), 0);

        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            moveDirection = new Vector3(CountX(), 0,
                                    CountZ());
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.Normalize();
            moveDirection *= speed;
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }

    void Update()
    {
        if (ax == 1 && gameController.inited)
        {
            Start2();
        }
        if (!gameController.paused && p && gameController.inited)
            Play();
    }
}
