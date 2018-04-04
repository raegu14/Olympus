using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoom : MonoBehaviour {

    public float thisCam;
    private GameObject cam;

	// Use this for initialization
	void Start () {
        cam = GameObject.FindGameObjectWithTag("MainCamera");	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        cam.GetComponent<Camera>().orthographicSize = thisCam;

    }
    void OnTriggerExit2D(Collider2D col)
    {
        cam.GetComponent<Camera>().orthographicSize = 15;
    }
}
