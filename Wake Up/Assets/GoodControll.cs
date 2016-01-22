using UnityEngine;
using System.Collections;
using System;

public class GoodControll : MonoBehaviour {

    private GameObject Game;
    public int nowG = 1;
    const float epsilon = 0.1f;
    public bool p = true;

    public float aimX, aimZ, aimRot;
    public int ax;

    // Use this for initialization
    void Start()
    {
        Game = GameObject.Find("/Game");
        GameObject.Find("/Player").GetComponent<HPcounter>().typeOb = 1;
        aimX = transform.position.x;
        aimZ = transform.position.z;
        aimRot = transform.rotation.y;
        ax = Game.GetComponent<gameController>().gLen;
        for (int i = 0; i < Game.GetComponent<gameController>().gLen; i++)
        {
            if (Mathf.Abs(aimX - Game.GetComponent<gameController>().gCordx[i]) < epsilon && Mathf.Abs(aimZ - Game.GetComponent<gameController>().gCordz[i]) < epsilon)
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

    float speed = 6.0f;
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
        int l = Game.GetComponent<gameController>().gELen[nowG];
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
                    if (a[j] == Game.GetComponent<gameController>().gEdge[nowG][i])
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
            for (int j = 0; j < memory; j++)
            {
                if(k == 0)
                {
                    nowG = Game.GetComponent<gameController>().gEdge[nowG][i];
                    aimX = Game.GetComponent<gameController>().gCordx[nowG];
                    aimZ = Game.GetComponent<gameController>().gCordz[nowG];
                    return;
                }
                if (visit[i] || b)
                {
                    k--;
                    break;
                }
            }
        }
    }

    float CountX()
    {
        if(Mathf.Abs(aimX - transform.position.x) < epsilon && Mathf.Abs(aimZ - transform.position.z) < epsilon)
        {
            Detect();
        }
        return aimX - transform.position.x;
    }

    float CountRotX()
    {
        if (Mathf.Abs(aimX - transform.position.x) < epsilon && Mathf.Abs(aimZ - transform.position.z) < epsilon)
        {
            Detect();
        }
        return aimRot - transform.rotation.y;
    }

    float CountZ()
    {
        if (Mathf.Abs(aimX - transform.position.x) < epsilon && Mathf.Abs(aimZ - transform.position.z) < epsilon)
        {
            Detect();
        }
        return aimZ - transform.position.z;
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
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }

    void Update()
    {
        if (!Game.GetComponent<gameController>().paused || p)
            Play();
    }
}
