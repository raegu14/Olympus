using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour {
	// All objects with the Burnable script should have the tag burnable

	// Constants
	//public float REGEN_TIME = 4f;
	float COOLDOWN = 1f;
	public float BURN_TIME = 2f;
	public float SMOKE_TIME = 2f;
	public float smoke_probability = 0.3f;
	public float smoke_probability_burn = 0.5f;

	// Public variables
	public float burnSpeed = 1;
	public float fuel = 10f;
  public float threshold = 100f; // threshold to burn
	public float burnDelay = 1f; // delay on burning
  public bool burning = false;

  // Private variables
	bool smoking = false;
	bool darken = false;
	bool cancel = false;
	bool playSound = true;
	float delayTimer;
	float burnTimer;
	public float cooldown = 0;
	float color = 0.3f;
	Color shade;
	GameObject player;
	Player p;
	SpriteRenderer rend;
 	GameObject particle;
	GameObject fire;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		p = player.GetComponent<Player>();
		//regenTimer = REGEN_TIME;
		burnTimer = BURN_TIME;
		delayTimer = SMOKE_TIME;
		float denom = SMOKE_TIME*2;
		shade = new Color(1f/denom, 1f/denom, 1f/denom, 0f);
		rend = this.GetComponent<SpriteRenderer>();
		particle = GameObject.Find("Circle Particle");
		fire = GameObject.Find("FireAnimation");
		if (gameObject.transform.name.StartsWith("Grass"))
		{
			playSound = false;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(burning)
		{
			burnTimer -= Time.deltaTime*burnSpeed;
			p.GainHP(Time.deltaTime*fuel/BURN_TIME);
			if(burnTimer <= 0f)
			{
				Burn();
			}
		}
		if(smoking)
		{
			if(threshold < p.hp)
				color = 0.2f;
			else
				color = 0.3f;
			if(cancel)
			{
				cooldown -= Time.deltaTime;
				if(cooldown <= 0 && !burning)
				{
					CancelSmoke();
				}
			}
			SpawnSmoke(burning ? smoke_probability_burn : smoke_probability);
			if(delayTimer > 0)
				delayTimer -= Time.deltaTime;
			else if(!burning && threshold < p.hp)
			{
				if(this.GetComponent<Vine>() != null)
					this.GetComponent<Vine>().Transport();
				else
					BeginBurn();
			}
		}
		if(darken)
		{
			rend.color -= shade * Time.deltaTime;
		}
		else if(rend.color != Color.white)
			rend.color += shade;
	}

/*	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Player" && !burning)
		{
			if(!smoking)
				BeginSmoke();
		}
	}

	void OnCollisionExit2D(Collision2D col)
	{
		if(col.gameObject.tag == "Player" && smoking)
		{
			if(SMOKE_TIME > 0)
				StartCancel();
		}
	}
    */
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player" && !burning)
		{
			if(!smoking)
				BeginSmoke();
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if(col.tag == "Player" && smoking)
		{
			if(SMOKE_TIME > 0)
				StartCancel();
		}
	}

	void BeginBurn()
	{
		burnTimer = BURN_TIME;
		burning = true;
		if(GetComponent<MassiveBurn>() != null)
			GetComponent<MassiveBurn>().BurnBB();
		if(playSound || Random.value < 0.01f)
		{
			GameObject temp = Instantiate(fire);
			if(!playSound)
				temp.transform.localScale = new Vector3(this.transform.localScale.x*3, this.transform.localScale.y, 0);
			GameObject.FindWithTag("AudioHandler").GetComponent<AudioHandler>().Play("ignite");
			SpriteRenderer tr = temp.GetComponent<SpriteRenderer>();
			Vector3 bounds = tr.bounds.extents;
			temp.transform.position = this.transform.position + new Vector3(0, bounds.y/2, 0);
			temp.transform.parent = this.transform;
		}
	}

	public void Burn()
	{
		Destroy(this.gameObject);
	}

	void BeginSmoke()
	{
		smoking = true;
		if(threshold < p.hp)
		{
			darken = true;
		}
	}

	void StartCancel()
	{
		cooldown = COOLDOWN;
		cancel = true;
	}

	void CancelSmoke()
	{
		if(!burning)
		{
			delayTimer = SMOKE_TIME;
			smoking = false;
			darken = false;
			cancel = false;
		}
	}

	void SpawnSmoke(float probability)
	{
		if(Random.value > probability)
			return;
		GameObject temp = Instantiate(particle);
		Vector3 pos = transform.position;
		if(this.GetComponent<Vine>() != null)
		{
			Vine v = this.GetComponent<Vine>();
			pos = transform.TransformPoint(v.start.offset);
			temp.GetComponent<Particle>().Init(pos, color, 0.1f, 1f, true);
			return;
		}
		Vector3 size = rend.bounds.size;
		float s = Mathf.Log(size.x * size.y);
		if(s < 1)
			s = 1;
		else if(s > Mathf.PI)
			s = Mathf.PI;
		temp.GetComponent<Particle>().Init(gameObject, pos, color, 0.1f*s);
	}


}
