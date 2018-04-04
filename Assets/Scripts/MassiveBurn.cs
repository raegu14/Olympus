using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassiveBurn : MonoBehaviour {

	GameObject fire;

	// Use this for initialization
	void Start () {
		fire = GameObject.Find("FireAnimation");
	}

	public void BurnBB ()
	{
		foreach(Transform child in transform)
		{
			if(!child.transform.name.StartsWith("FireSpawn"))
				return;
			Vector3 bounds = child.GetComponent<SpriteRenderer>().bounds.extents;
			Vector2 pos = Random.insideUnitCircle;
			Vector3 boundary = new Vector3(pos.x * bounds.x, pos.y * bounds.y, 0);
			GameObject f = Instantiate(fire);
			f.transform.localScale *= Random.value + 1;
			f.transform.position = child.transform.position + boundary;
			f.transform.parent = this.transform;
		}
	}
}
