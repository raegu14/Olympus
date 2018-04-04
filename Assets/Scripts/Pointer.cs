using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {

	public GameObject[] particles;
	public GameObject sparks;
	Camera cam;

	// Use this for initialization
	void Start () {
		cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		sparks = new GameObject("MenuSparks");
		sparks.transform.position = cam.transform.position;
		sparks.transform.parent = this.transform.parent;
	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
		SpawnSparks(pos);
		pos.z += 3f;
		transform.position = pos;
	}

	void SpawnSparks(Vector3 pos)
	{
		pos.z += 3f;
		for(int i = 0; i < 3; i++)
		{
			for(int j = 0; j < particles.Length; j++)
			{
				GameObject obj = Instantiate(particles[j]);
				Particle temp = obj.GetComponent<Particle>();
				temp.Init(pos, 1f, 1f, 0.1f, true);
				temp.Set(5f, 0.2f, false);
				temp.transform.parent = sparks.transform;
			}
		}
	}
}
