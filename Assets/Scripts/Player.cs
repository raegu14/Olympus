using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Constants
	public float SCALE = 2000;
	public float DEATH_FLOOR = 100; // die at this point
	public float PARTICLE_PROBABILITY = 0.5f; // spawn fire particles
	//float jumpHeight = 5.0f;
	float MARGIN = .1f;

	//components
	Rigidbody2D rb;
	public GameObject[] particles;

	// Stats
	public float hp = 1000; // current hp
	public float speed = 50;
	public float burnSpeed = 2;

	float boost; //counter gravity
	float transportSpeed = 5;

	public bool move = true;
	bool active = true;
	float slope;
	float hpTickUp = 0;
	Vector2 dest;
	Vector2 dir;
	GameObject vine;

	//animation
	SpriteRenderer sr;
	Animator anim;
	bool isWalking;
	bool left;


	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(active)
		{
			hp -= Time.deltaTime * burnSpeed;
			anim.SetFloat("HP", hp);

			if(hp < DEATH_FLOOR)
			{
				GameObject.FindWithTag("MainCamera").GetComponent<Menu>().FadeOut();
			}

			PARTICLE_PROBABILITY = hp / (SCALE * 2);
			for (int i = 0; i < particles.Length; i++)
			{
				if (Random.value < PARTICLE_PROBABILITY)
				{
					GameObject temp = Instantiate(particles[i]);
					temp.GetComponent<Particle>().SetBound(sr.bounds.extents);
					temp.GetComponent<Particle>().Init(gameObject, hp / (DEATH_FLOOR * 2.0f), 5f, 2f);
				}
			}

			transform.localRotation = Quaternion.Euler(0, 0, 0);
			if(move)
			{
				anim.SetBool("Walking", false);
				if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
				{
					anim.SetBool("Walking", true);
					sr.flipX = true;
					anim.SetBool("Left", true);
					if (slope < 0)
					{
						rb.AddForce(new Vector2(-1, -slope).normalized * speed * boost);
					}
					else
					{
						rb.AddForce(new Vector2(-1, -slope).normalized * speed);
					}
				}
				if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
				{
					anim.SetBool("Walking", true);
					sr.flipX = false;
					if (slope > 0)
					{
						rb.AddForce(new Vector2(1, slope).normalized * speed * boost);
					}
					else
					{
						rb.AddForce(new Vector2(1, slope).normalized * speed);
					}
				}
				rb.velocity = new Vector2(rb.velocity.x > speed ? speed : rb.velocity.x, rb.velocity.y);
			}
			else
			{
				transform.localRotation = Quaternion.Euler(0, 0, 90 + (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg));
                anim.SetBool("Vine", true);
				// forcibly moved somewhere
				float posx = transform.position.x + dir.x*Time.deltaTime*transportSpeed;
				float posy = transform.position.y + dir.y*Time.deltaTime*transportSpeed;
				transform.position = new Vector3(posx, posy, 0.0f);
				hp += hpTickUp;
				if(transform.position.y > dest.y-MARGIN && transform.position.y < dest.y+MARGIN
				|| transform.position.x > dest.x-MARGIN && transform.position.x < dest.x+MARGIN)
				{
					move = true;
					GetComponent<CapsuleCollider2D>().enabled = true;
					rb.gravityScale = 10.0f;
                    if(vine != null)
					    vine.GetComponent<Burnable>().Burn();
					anim.SetBool("Vine", false);
				}
			}
			//update size
			if(hp > DEATH_FLOOR)
				transform.localScale = new Vector3(hp/SCALE, hp/SCALE);
		}
	}

	public void Move(Vector2 start, Vector2 stop, GameObject aVine, float tick)
	{
		GetComponent<CapsuleCollider2D>().enabled = false;
		move = false;
		dest = stop;
		dir = stop-start;
		dir.Normalize();
		rb.gravityScale = 0f;
		rb.velocity = dir;
		vine = aVine;
		hpTickUp = tick;
        transportSpeed = 5;
    }

    public void Move(Vector2 start, Vector2 stop, GameObject aVine, float tick, float speed)
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        move = false;
        dest = stop;
        dir = stop - start;
        dir.Normalize();
        rb.gravityScale = 0f;
        rb.velocity = dir;
        vine = aVine;
        hpTickUp = tick;
        transportSpeed = speed;
    }

    public void GainHP(float gain)
	{
		hp += gain;
	}


	void OnCollisionEnter2D(Collision2D col)
	{
        if (col.gameObject.tag == "ground")
        {
            var theta = col.gameObject.transform.rotation.eulerAngles.z;
            if (theta > 180)
            {
                theta -= 180;
            }
            var acuteTheta = Mathf.Min(theta, 180 - theta);
            if (acuteTheta < 30)
            {
                slope = Mathf.Tan(Mathf.Deg2Rad * theta);
                boost = Mathf.Abs(acuteTheta) / 20 + 1;
            }
        }
	}

	public void SetActive(bool b)
	{
		active = b;
	}
}
