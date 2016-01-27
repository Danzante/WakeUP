using UnityEngine;
using System.Collections;

public class PBcontroller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        gameController.Pause();
    }
}
