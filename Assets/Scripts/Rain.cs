using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour {

    private float damage = 1f; //subtracted from player hp
    private float angle = 10;    //x-dimension fall angle
    private float fallspeed = -50f;

    private Vector3 prevPos;
    private Vector3 movement;

	// Use this for initialization
	void Start () {
        prevPos = transform.position;
        movement = new Vector3(-angle, fallspeed, 0);
	}

	// Update is called once per frame
	void FixedUpdate () {
        //move down
        prevPos = transform.position;
        transform.Translate(movement * Time.deltaTime);
	}

    //collide with objects
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().GainHP(-damage);
        }
        if(col.gameObject.tag == "burning")
        {
            //col.gameObject.GetComponent<Burnable>().decrementFire();
        }
        movement = new Vector3(-angle/10, -fallspeed/10, 0);
        //play animation
        Invoke("Evaporate", 0.1f);
    }

    void Evaporate()
    {
        Destroy(gameObject);
    }
}
