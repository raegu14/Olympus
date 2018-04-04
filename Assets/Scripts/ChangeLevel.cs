using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour {

	public int id;
	Meta meta;
	bool collided = false;

	// Use this for initialization
	void Start () {
		meta = GameObject.Find("META").GetComponent<Meta>();
		if(id < meta.level)
			collided = true;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player" && !collided)
		{
			meta = GameObject.Find("META").GetComponent<Meta>();
			meta.hp = col.GetComponent<Player>().hp;
			meta.level++;
			meta.Able();
			collided = true;
		}
	}
}
