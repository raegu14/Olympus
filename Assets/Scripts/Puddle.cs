using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour {

    //constants
    public float damage = 1f; //
    public float STEAM_PROBABILITY = 0.5f;
    //public float MARGIN = 0.01f;
    public float damagePerSec = 1f;

    public GameObject player;
    GameObject particle;
    AudioHandler aud;
    Vector3 originalPosition;

    //evaporation
    bool evaporating = false;
    float rateOfEvap = 0.1f;
    SpriteRenderer rend;


	// Use this for initialization
	void Start () {
        aud = GameObject.FindWithTag("AudioHandler").GetComponent<AudioHandler>();
        rend = GetComponent<SpriteRenderer>();
        originalPosition = transform.position;
        particle = GameObject.Find("Circle Particle");
	}

	// Update is called once per frame
	void FixedUpdate () {
        if (evaporating)
        {
            if(Random.value > STEAM_PROBABILITY)
            {
              SpawnSteam();
            }
            player.gameObject.GetComponent<Player>().GainHP(-damage*damagePerSec*Time.deltaTime);
            Vector3 pos = gameObject.transform.position;
            pos.y -= Time.deltaTime*rateOfEvap;
            gameObject.transform.position = pos;
            if(pos.y + rend.bounds.size.y < originalPosition.y)
              evaporating = false;
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
      if(col.tag == "Player" &&
        transform.position.y + rend.bounds.size.y  >= originalPosition.y)
        {
          evaporating = true;
          aud.Play("sizzle");
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        print(col.gameObject.tag);
      if(col.tag == "Player")
        evaporating = false;
    }

    void SpawnSteam()
    {
      GameObject temp = Instantiate(particle);
      temp.GetComponent<Particle>().Init(player, 0.2f, 1f, 0.75f);
    }
}
