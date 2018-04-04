using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour {

	public Menu menu;
	public Color original = Color.white;
	public Color hover = Color.red;
	public string action;
	public GameObject pointer;
	SpriteRenderer text;

	// Use this for initialization
	void Start () {
		text = GetComponent<SpriteRenderer>();
	}

	void OnMouseOver()
	{
		text.color = hover;
		//pointer.SetActive(true);
		if(Input.GetMouseButtonDown(0))
		{
			if(action == "restart")
			{
				menu.Restart();
			}
			else if(action == "resume")
			{
				menu.Unpause();
			}
			else if(action == "title")
			{
				menu.TitleScreen();
			}
			else if(action == "quit")
			{
				Application.Quit();
			}
		}
	}

	void OnMouseExit()
	{
		text.color = original;
		//pointer.SetActive(false);
	}
}
