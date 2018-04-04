using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    public int state;
    public float timer;
    public float offset;

    public GameObject[] bossStates;

    public GameObject p;
    public GameObject knockBack;

    //lake
    public GameObject lake;
    private float lakeMax = -5.18001f;

    //falling ceiling
    public GameObject[] roofs;
    public float[] roofEnds;
    private float fallConst = 0.1f;
    AudioHandler aud;


    //fade to black
    public GameObject black;

    private float r;
    private float g;
    private float b;
    private float a;

    private float alphaIncrement = .01f;

    // Use this for initialization
    void Start () {
        aud = GameObject.FindGameObjectWithTag("AudioHandler").GetComponent<AudioHandler>();
        timer = Time.time;
        p = GameObject.FindGameObjectWithTag("Player");
        r = black.GetComponent<SpriteRenderer>().color.r;
        g = black.GetComponent<SpriteRenderer>().color.g;
        b = black.GetComponent<SpriteRenderer>().color.b;
        a = 0.0f;
        for(int i = 0; i < roofs.Length; i++)
        {
            roofEnds[i] = roofs[i].transform.position.y - roofEnds[i];
        }
        lakeMax = lake.transform.position.y - lakeMax;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if(state == 1)
        {
            for(int i = 0; i < roofs.Length; i++)
            {
                Vector3 dest = roofs[i].transform.position;
                dest.y = Mathf.Max(dest.y - fallConst, roofEnds[i]);
                roofs[i].transform.position = dest;
            }
            fallConst *= 1.03f;

            Vector3 lakePos = lake.transform.position;
            print(lakeMax);
            lakePos.y = Mathf.Min(lakePos.y + 0.05f, lakeMax);
            print(lakePos.y);
            lake.transform.position = lakePos;
        }

        if (state < 3)
        {
            if(bossStates[state] == null)
            {
                state++;
                if(state < 3)
                {
                    p.GetComponent<Player>().Move(p.transform.position, knockBack.transform.position, null, 0, 50);
                    aud.Play("wind");
                }
            }
        }
        if(state < 3)
        {
            bossStates[state].SetActive(true);
        }

        if((state == 2 && bossStates[2].GetComponent<Burnable>().burning) || state == 3)
        {
            a += alphaIncrement;
            black.GetComponent<SpriteRenderer>().color = new Color(r, g, b, a);
        }
        if(state == 3)
        {
            aud.Play("creak");
            GameObject.Find("META").GetComponent<Meta>().End();
        }

		if(Time.time > timer)
        {
            timer = Time.time + offset;
            //move things around
        }
	}
}
