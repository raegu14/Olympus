using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach this to an object named Title
public class Title : MonoBehaviour {

	int strike = 0;
	int flamePoint = 5;
	Menu menu;
	Camera cam;
	Player player;
	TextMesh text;
	Meta meta;
	Color increment;
	public GameObject[] particles;
	public GameObject sparkLight;
	AudioHandler aud;

	// Use this for initialization
	void Start () {
		GameObject temp = GameObject.FindWithTag("MainCamera");
		menu = temp.GetComponent<Menu>();
		cam = temp.GetComponent<Camera>();
		player = GameObject.FindWithTag("Player").GetComponent<Player>();
		player.SetActive(false);
		text = this.GetComponent<TextMesh>();
		Color diff = Color.white - text.color;
		increment = diff/flamePoint;
		meta = GameObject.Find("META").GetComponent<Meta>();
		aud = GameObject.FindWithTag("AudioHandler").GetComponent<AudioHandler>();
	}

	// Update is called once per frame
	void Update () {
		if(meta.level >= 0 || strike >= flamePoint)
		{
			player.SetActive(true);
			if(meta.level < 0)
			{
				Vector3 temp = cam.ScreenToWorldPoint(Input.mousePosition);
				temp.z = 0f;
				player.transform.position = temp;
				meta.level++;
				meta.Able();
			}
			else
			{
				player.transform.position = meta.levelPos[meta.level].transform.position;
				player.hp = meta.hp;
				cam.GetComponent<CamMove>().SetZoom((meta.level+1)*5f);
			}

			menu.Unpause();
			aud.Play("fwoosh");
			aud.Play("crackle");
			Destroy(gameObject);
		}
		if(Input.GetMouseButtonDown(0))
		{
			strike++;
			aud.Play("lighter");
			text.color += increment;
			SpawnSparks(Input.mousePosition);
		}
	}


	void SpawnSparks(Vector3 rawPos)
	{
		Vector3 pos = cam.ScreenToWorldPoint(rawPos);
		pos.z = 1f;
		GameObject sparks = new GameObject("Sparks");
		for(int i = 0; i < strike*strike; i++)
		{
			for(int j = 0; j < particles.Length; j++)
			{
				GameObject obj = Instantiate(particles[j]);
				Particle temp = obj.GetComponent<Particle>();
				temp.Init(pos, 1f, 1.2f, 0.5f, false);
				temp.Set(10f, 10f, true);
				obj.transform.parent = sparks.transform;
			}
		}
		pos.z = -3f;
		GameObject light = Instantiate(sparkLight);
		light.transform.parent = sparks.transform;
		light.transform.position = pos;
	}

	private IEnumerator WaitForAnimation(Animation animation)
	{
		while(animation.isPlaying)
			yield return null;
	}

}
