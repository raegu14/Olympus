using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

    public GameObject rain;
    public float range = 2.5f;

	// Use this for initialization
	void Start () {
        range *= transform.localScale.x;
	}

	// Update is called once per frame
	void FixedUpdate () {
        float pos = Random.Range(-range, range) + transform.position.x;
        //if (Random.value < 0.9f)
        {
            Instantiate(rain, new Vector3(pos, transform.position.y, transform.position.z), rain.transform.localRotation);
        }
	}
}
