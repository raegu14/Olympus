using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour {

    public GameObject player;

    //flicker parameters
    private Light lit;
    private float minIntensity = 7;
    private float maxIntensity = 7.5f;
    private float step = 2f;
    private float wait;
    private bool increasing = false; //flame intensity is increasing

    private float baseHp;

	// Use this for initialization
	void Start () {
        lit = GetComponent<Light>();
        baseHp = player.GetComponent<Player>().hp;
	}

	// Update is called once per frame
	void FixedUpdate () {
        transform.position = player.transform.position - new Vector3(0, 0, 3);
        //scales to player size
        lit.range = player.GetComponent<Player>().hp / 50;
        UpdateIntensity();
	}

    void UpdateIntensity()
    {
        if (Time.time > wait)
        {
            if (increasing)
                lit.intensity = lit.intensity + step;
            else
                lit.intensity = lit.intensity - step;
        }

        if (lit.intensity > maxIntensity)
        {
            increasing = false;
            wait = Random.Range(0.1f, 0.5f) + Time.time;
            //print(wait);
            lit.intensity = maxIntensity;
        }

        if (lit.intensity < minIntensity)
        {
            increasing = true;
            wait = Random.Range(0.1f, 0.2f) + Time.time;
            //print(wait);

            lit.intensity = minIntensity;
        }
    }
}
