using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Main : MonoBehaviour
{
	UnityEngine.Object prefab;

	List<Bg> bgs = new List<Bg> ();

	void Start ()
	{
		prefab = Resources.Load ("Prefabs/proto_bg0");
	}

	void Update ()
	{
		if (Time.frameCount % 30 == 0) {
			Bg bg = new Bg (Instantiate (prefab) as GameObject);
			bgs.Add (bg);
			bg.obj.transform.localScale = new Vector3 (.01f, .01f);
			bg.obj.transform.localEulerAngles = new Vector3 (0, 0, UnityEngine.Random.Range (0, 360));
		}

		float p = 10 * Mathf.Min (.35f, Mathf.Sqrt (
			          Mathf.Pow (Input.mousePosition.x / Screen.height - .5f, 2) +
			          Mathf.Pow (Input.mousePosition.y / Screen.height - .5f, 2)));
		float phi = 
			Mathf.Atan2 (Input.mousePosition.y / Screen.height - .5f, Input.mousePosition.x / Screen.height - .5f);
		Debug.Log (p);

		foreach (var bg in bgs) {
			bg.obj.transform.localPosition = new Vector3 (
				p * Mathf.Cos (phi) * bg.obj.transform.localScale.x,
				p * Mathf.Sin (phi) * bg.obj.transform.localScale.x
			);
			bg.obj.transform.localScale = 
				new Vector3 (bg.obj.transform.localScale.x * bg.speed, bg.obj.transform.localScale.y * bg.speed);
		}

		for (var i = bgs.Count - 1; i >= 0; i--)
			if (bgs [i].obj.transform.localScale.x > 10) {
				Destroy (bgs [i].obj);
				bgs.RemoveAt (i);
			}
	}

}

class Bg
{
	public GameObject obj;
	public float speed = .005f * UnityEngine.Random.value + 1.025f;

	public Bg (GameObject obj)
	{
		this.obj = obj;
	}

}
