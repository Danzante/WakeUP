using UnityEngine;
using System.Collections;

public class ShootControll : MonoBehaviour
{

    float rotationY;
    public int weapon = 0; // 0 for revolver, 2 for double-barreled gun, 1 for carcass(арматура)
    public float scroll;
    private GameObject Head;
    private GameObject Body;
    private GameObject Game;
    bool[] has = new bool[3];
    int[] nowB = new int[3];
    int[] totalB = new int[3];
    const float epsilon = 0.01f;

    // Use this for initialization
    void Start()
    {
        Body = transform.parent.gameObject;
        Head = GameObject.Find("/Player/Head");
        Game = GameObject.Find("/Game");
        has[0] = true;
        nowB[0] = 6;
        totalB[0] = 0;
        for (int i = 1; i < 3; i++)
        {
            nowB[i] = 0;
            totalB[i] = 0;
            has[i] = false;
        }
    }

    void Recharge()
    {
        if (weapon == 0)
        {
            if (nowB[0] == 0)
            {
                if (totalB[0] >= 6)
                {
                    totalB[0] -= 6;
                    nowB[0] = 6;
                }
                else
                {
                    nowB[0] = totalB[0];
                    totalB[0] = 0;
                }
            }
        }
        if (weapon == 2)
        {
            if (totalB[2] >= 2)
            {
                totalB[2] -= 2;
                nowB[2] = 2;
            }
            else
            {
                nowB[2] = totalB[0];
                totalB[2] = 0;
            }
        }
    }

    void ShootRevolver()
    {
        if(nowB[0] == 0)
        {
            if (totalB[0] == 0)
                return;
            Recharge();
        }
        Ray myray = new Ray(transform.position, transform.forward);
        RaycastHit help;
        if (Physics.Raycast(myray, out help))
        {
            Debug.Log(help.point);
            help.collider.gameObject.GetComponent<HPcounter>().GetStrike_Heall(16);
        }
    }

    void ShootDBG()
    {
        if (nowB[2] == 0)
        {
            if (totalB[2] == 0)
                return;
            Recharge();
        }
        Ray myray = new Ray(transform.position, transform.forward);
        RaycastHit help;
        if (Physics.Raycast(myray, out help))
        {
            Debug.Log(help.point);
            help.collider.gameObject.GetComponent<HPcounter>().GetStrike_Heall(50);
        }
    }

    void Strike()
    {
        Ray myray = new Ray(transform.position, transform.forward);
        RaycastHit help;
        if (Physics.Raycast(myray, out help, 1))
        {
            Debug.Log(help.point);
            help.collider.gameObject.GetComponent<HPcounter>().GetStrike_Heall(34);
        }
    }

    // Update is called once per frame
    void Play()
    {
        rotationY = Input.GetAxis("Mouse Y") * 10F;
        if (((Mathf.Abs(Vector3.Angle(Head.transform.forward, Body.transform.forward) - rotationY) < 50) && (Vector3.Angle(Head.transform.forward, Body.transform.up) > 90)) || ((Mathf.Abs(Vector3.Angle(Head.transform.forward, Body.transform.forward) + rotationY) < 70) && (Vector3.Angle(Head.transform.forward, Body.transform.up) <= 90)))
            Head.transform.Rotate(new Vector3(-rotationY, 0, 0));

        scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) > epsilon)
        {
            int k = 0;
            for (int i = 0; i < 3; i++)
                if (has[i])
                    k++;
            weapon = (weapon + (int)Mathf.Round(scroll * 10))% k;
            if (weapon < 0)
                weapon += k;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Recharge();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(weapon == 0)
                ShootRevolver();
            if (weapon == 2)
                ShootDBG();
            if (weapon == 1)
                Strike();
        }
    }

    void Update()
    {
        if (!Game.GetComponent<gameController>().paused)
            Play();
    }
}
