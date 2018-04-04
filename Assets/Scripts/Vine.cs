using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour {

	public float hpTick = 10;
	bool transporting = false;

	public BoxCollider2D start;
	public BoxCollider2D dest;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void Transport()
	{
		if(!transporting)
		{
			transporting = true;
			Player p = GameObject.FindWithTag("Player").GetComponent<Player>();
			p.Move(transform.TransformPoint(start.offset),
				transform.TransformPoint(dest.offset), this.gameObject, hpTick);
			GameObject temp = Instantiate(GameObject.Find("FireAnimation"));
			GameObject.FindWithTag("AudioHandler").GetComponent<AudioHandler>().Play("ignite");
			SpriteRenderer tr = temp.GetComponent<SpriteRenderer>();
			Vector3 bounds = tr.bounds.extents;
			temp.transform.position = this.transform.position + new Vector3(0, bounds.y/2, 0);
			temp.transform.parent = this.transform;
		}

	}

}
