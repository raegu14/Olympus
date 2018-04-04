using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Topple : MonoBehaviour {

    //trigger to rotate
    public bool moving = false;

    //logs to burn
    public GameObject log1;
    public GameObject log2;

    //rotation components
    private float currIterations = 1;
    private float maxIterations = 100;
    private Quaternion initial;
    private Quaternion target;

    public bool finished = false;

	// Use this for initialization
	void Start () {
        initial = transform.localRotation;
        target = Quaternion.Euler(0, 0, -57);
	}
	
	// Update is called once per frame
	void Update () {
        if(log1 == null && (log2 == null || log2.GetComponent<Burnable>().burning))
        {
            moving = true;
        }

        if (moving && currIterations <= maxIterations) {
            gameObject.transform.localRotation = Quaternion.Lerp(initial, target, currIterations/maxIterations);
            currIterations *= 1.03f;
        }
        if(currIterations > maxIterations)
        {
            finished = true;
        }
	}
}