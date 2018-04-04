using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToGround : MonoBehaviour {

    public bool ground;
    public GameObject topple;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void FixedUpdate () {
        if (topple.GetComponent<Topple>().finished)
        {
            if (ground)
                tag = "Untagged";
            else
                tag = "ground";
        }
	}
}
