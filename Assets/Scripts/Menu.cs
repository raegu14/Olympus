using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Attach this to the main camera
public class Menu : MonoBehaviour {

	public GameObject menu;
	bool paused = false;
	bool pausible = true;
	CamMove cam;
	Meta meta;

  // Fade out
  public Texture2D fadeTexture;
  public float fadeSpeed = 0.2f;
  public int drawDepth = -1000;
  float alpha = 0f;
  bool fade = false;

	// Use this for initialization
	void Start () {
		cam = GameObject.FindWithTag("MainCamera").GetComponent<CamMove>();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(pausible)
			{
				if(!paused)
					Pause();
				else
					Unpause();
			}
			else if(GameObject.Find("Title") != null)
			{
				Application.Quit();
			}
		}
	}

	void OnGUI()
	{
		// Fade to black and restart level
		if(fade)
		{
			// player no longer affects surroundings
			alpha += fadeSpeed * Time.deltaTime;
			Color c = GUI.color; c.a = alpha;
			GUI.color = c;
			GUI.depth = drawDepth;
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
			if(c.a >= 1f)
			{
				Restart();
			}
		}
	}

	public void Pause()
	{
		Time.timeScale = 0;
		paused = true;
		menu.SetActive(true);
	}
	public void Unpause()
	{
		Time.timeScale = 1;
		paused = false;
		pausible = true;
		menu.SetActive(false);
		cam.Track();
		Destroy(GameObject.Find("MenuSparks"));
	}
	public void Restart()
	{
		SceneManager.LoadScene("Level_0");
	}
	public void TitleScreen()
	{
		foreach(GameObject m in GameObject.FindGameObjectsWithTag("meta"))
		{
			Destroy(m);
		}
		SceneManager.LoadScene("Level_0");
	}

	public void FadeOut()
	{
		GameObject.FindWithTag("Player").GetComponent<Player>().SetActive(false);
		fade = true;
	}

}
