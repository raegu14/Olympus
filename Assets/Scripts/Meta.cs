using UnityEngine;
using System.Collections;

public class Meta : MonoBehaviour {

	public int level = -1;
	public float hp = 250;
	public GameObject[] levelPos;
	public GameObject[] toDisable;
	public AudioSource aud;
	public AudioClip titleBGM;
	public AudioClip[] levelBGM;
	public AudioClip bossBGM;
	public AudioClip postBossBGM;
	CamMove cam;

	AudioHandler handle;

	// Make sure there aren't any duplicates
	Meta instance;

  void Awake() {
		if(instance == null && GameObject.FindGameObjectsWithTag("meta").Length > 2)
		{
			Destroy(gameObject);
			return;
		}
		Set();
		instance = this;
    DontDestroyOnLoad(transform.gameObject);
		Able();
  }

	void Update()
	{
		if(toDisable[0] == null)
		{
			Set();
			Able();
		}
	}

	void Set()
	{
		cam = GameObject.FindWithTag("MainCamera").GetComponent<CamMove>();
		handle = GameObject.FindWithTag("AudioHandler").GetComponent<AudioHandler>();
		foreach (GameObject d in GameObject.FindGameObjectsWithTag("disable"))
		{
			string name = d.transform.name;
			string last = name.Substring(name.Length - 1, 1);
			toDisable[int.Parse(last)-1] = d;
		}
	}

	public void Able()
	{
		if(level == -1)
		{
			aud.clip = titleBGM;
			for(int i = 1; i < toDisable.Length; i++)
				toDisable[i].SetActive(false);
		}
		else if(level == 3)
		{
			aud.clip = bossBGM;
			for(int i = 0; i < toDisable.Length-1; i++)
				toDisable[i].SetActive(false);
		}
		else
		{
			if(hp > 0)
			{
				GameObject.FindWithTag("Player").GetComponent<Player>().hp = hp;
			}
			for(int i = 0; i < toDisable.Length; i++)
			{
				if(level == i)
				{
					aud.clip = levelBGM[i];
					toDisable[i].SetActive(true);
				}
				else
				{
					toDisable[i].SetActive(false);
				}
				if(level > 0)
				{
					handle.Play("rain");
				}
			}
			cam.Zoom((level+1) * 5f);
		}
		aud.Play();
	}

	public void End()
	{
		aud.clip = postBossBGM;
		aud.Play();
	}
}
