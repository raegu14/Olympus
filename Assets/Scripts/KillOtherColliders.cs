using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOtherColliders : MonoBehaviour {
    public GameObject[] colliders;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (!GetComponent<SpriteRenderer>().enabled)
        {
            for (int i = 0; i < colliders.Length; i++) {
                Destroy(colliders[i]);
            }
        }
	}
}
