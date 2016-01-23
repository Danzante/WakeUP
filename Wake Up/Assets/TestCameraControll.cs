using UnityEngine;
using System.Collections;

public class TestCameraControll : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    float rotSpeed = 900;

    // Update is called once per frame
    void Update () {
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime, 0);
    }
}
