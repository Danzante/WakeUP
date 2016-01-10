using UnityEngine;
using System.Collections;

public class ShootControll : MonoBehaviour
{

    float rotationY;
    private GameObject Head;
    private GameObject Body;

    // Use this for initialization
    void Start()
    {
        Body = transform.parent.gameObject;
        Head = GameObject.Find("/Player/Head");
    }

    void Shoot()
    {
        Ray myray = new Ray(transform.position, transform.forward);
        RaycastHit help;
        if (Physics.Raycast(myray, out help))
        {
            Debug.Log(help.point);
            help.collider.gameObject.GetComponent<HPcounter>().GetStrike_Heall(10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        rotationY = Input.GetAxis("Mouse Y") * 10F;
        if (((Mathf.Abs(Vector3.Angle(Head.transform.forward, Body.transform.forward) - rotationY) < 50) && (Vector3.Angle(Head.transform.forward, Body.transform.up) > 90)) || ((Mathf.Abs(Vector3.Angle(Head.transform.forward, Body.transform.forward) + rotationY) < 70) && (Vector3.Angle(Head.transform.forward, Body.transform.up) <= 90)))
            Head.transform.Rotate(new Vector3(-rotationY, 0, 0));
        
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Shoot();
        }
    }
}
