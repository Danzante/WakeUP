using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour {

    public bool paused { get;  private set; }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            paused = !paused;
        }
    }
}
