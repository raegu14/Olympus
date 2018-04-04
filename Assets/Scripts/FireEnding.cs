using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnding : MonoBehaviour {


    /* fade */
    private bool beginFade = false;
    public Texture2D fade;
    private float alpha = 0.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnGUI()
    {
        if (beginFade)
        {
            alpha += 0.5f * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);

            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
            GUI.depth = -1000;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fade);
        }
    }
}
