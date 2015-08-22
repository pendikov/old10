using UnityEngine;
using System.Collections;

public class Fish : Sprite {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0.0f, 0.0f, -200.0f * Time.deltaTime));
	}
}
