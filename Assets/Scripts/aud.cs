using UnityEngine;
using System.Collections;

public class aud : MonoBehaviour {

	public AudioSource audio;
	public float startingPitch = 4.0f;
	public float timeToDecrease = 5.0f;
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.frameCount % 40 == 0) {
			float x = Random.value * 3.0f;
			audio.pitch = x;
		}
	}
}
