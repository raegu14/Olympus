using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour {

	public AudioSource main;
	public AudioSource aux;
	public AudioSource ambient1;
	public AudioSource ambient2;
	public AudioSource persistent1;
	public AudioSource persistent2;
	public AudioSource random1;
	public AudioSource random2;

	public AudioClip lighter;
	public AudioClip fwoosh;
	public AudioClip burn;
	public AudioClip sizzle;
	public AudioClip rain;
	public AudioClip crackle;

	// Random periodic sounds
	public AudioClip[] wood;
	public AudioClip creak;
	public AudioClip wind;
	public AudioClip thunder;

	// Probability of random sounds
	bool canThunder = false;
	float thunderProbability = 0.003f;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		if(random1.isPlaying && random2.isPlaying)
			return;
		if(canThunder && Random.value < thunderProbability)
		{
			if(!random1.isPlaying)
			{
				random1.clip = thunder;
				random1.Play();
			}
			else
			{
				random2.clip = thunder;
				random2.Play();
			}
		}
	}

	public void Play(string name)
	{
		if(name == "lighter")
		{
			if(!main.isPlaying)
			{
				main.clip = lighter;
				main.Play();
			}
			else
			{
				aux.clip = lighter;
				aux.Play();
			}
		}
		else if(name == "fwoosh" || name == "ignite")
		{
			if(!main.isPlaying)
			{
				main.clip = fwoosh;
				main.Play();
			}
			else
			{
				aux.clip = fwoosh;
				aux.Play();
			}
		}
		else if(name == "sizzle")
		{
			ambient2.clip = sizzle;
			ambient2.Play();
		}
		else if(name == "rain")
		{
			persistent1.clip = rain;
			persistent1.Play();
			canThunder = true;
		}
		else if(name == "crackle")
		{
			persistent2.clip = crackle;
			persistent2.Play();
		}
		else if(name == "wind")
		{
			aux.clip = wind;
			aux.Play();
		}
		else if(name == "creak")
		{
			main.clip = creak;
			main.Play();
		}
	}
	public void Stop(string name)
	{
		if(name == "rain")
		{
			persistent1.Stop();
			canThunder = false;
		}
	}
}
