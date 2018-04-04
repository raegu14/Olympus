using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour {

	float dimSpeed = 12f;
	Light thisLight;
	// Use this for initialization
	void Start () {
		thisLight = GetComponent<Light>();
	}

	// Update is called once per frame
	void Update () {
		thisLight.intensity -= Time.deltaTime * dimSpeed;
		if(thisLight.intensity <= 0)
			Destroy(gameObject);
	}
}
