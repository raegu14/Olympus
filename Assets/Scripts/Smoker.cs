using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoker : MonoBehaviour {

	GameObject particle;
	float probability = 0.9f;
	SpriteRenderer rend;
	float timer = 0;

	// Use this for initialization
	void Start () {
		particle = GameObject.Find("Circle Particle");
		rend = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(timer < 20f)
		{
			Smoke();
			timer += Time.deltaTime;
		}
	}

	void Smoke()
	{
		if(Random.value > probability)
			return;
		GameObject temp = Instantiate(particle);
		Vector3 pos = transform.position;
		Vector3 size = rend.bounds.size;
		float s = Mathf.Log(size.x * size.y);
		if(s < 1)
			s = 1;
		else if(s > Mathf.PI)
			s = Mathf.PI;
		temp.GetComponent<Particle>().Init(gameObject, pos, 0.2f, 0.1f*s);
	}
}
