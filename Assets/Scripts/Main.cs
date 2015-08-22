using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
	UnityEngine.Object prefab;

	const float TRACK_SPEED = .01f;

	List<Layer> layers = new List<Layer> ();


	Vector3 track1 = new Vector3 (0, 0);
	Vector3 track2 = new Vector3 (0, 0);
	float trackTime = 0;

	void Start ()
	{
		prefab = Resources.Load ("Prefabs/proto_bg0");
	}

	void Update ()
	{
		if (Time.frameCount % 15 == 0) {
			Layer layer = new Layer (Instantiate (prefab) as GameObject);
			layers.Add (layer);
//			bg.obj.transform.localScale = new Vector3 (.01f, .01f);
			layer.obj.transform.localEulerAngles = new Vector3 (0, 0, Random.Range (0, 360));
			layer.obj.GetComponent<SpriteRenderer> ().color = new Color (
				Random.value,
				Random.value,
				Random.value
			);
		}

		trackTime += TRACK_SPEED;
		while (trackTime >= 1) {
			trackTime -= 1;
			track1 = track2;
			do {
				track2 = new Vector3 (
					2 * (Random.value - .5f),
					2 * (Random.value - .5f)
				);
			} while(H.Hypot (track2.x, track2.y) > 1);
		}

		Vector3 posTrack = 
			track1 * Mathf.Cos (trackTime * Mathf.PI / 2) +
			track2 * (1 - Mathf.Cos (trackTime * Mathf.PI / 2));

		float p = 10 * Mathf.Min (.3f, Mathf.Sqrt (
			          Mathf.Pow (Input.mousePosition.x / Screen.width - .5f, 2) +
			          Mathf.Pow (Input.mousePosition.y / Screen.height - .5f, 2)));
		float phi = 
			Mathf.Atan2 (Input.mousePosition.y / Screen.height - .5f, Input.mousePosition.x / Screen.width - .5f);

		foreach (var layer in layers) {
			layer.pos += .01f;
			var exp = .01f * Mathf.Pow (1.05f, 100 * layer.pos);
			layer.obj.transform.localPosition = new Vector3 (
				p * Mathf.Cos (phi) * exp + 1 / layer.pos * posTrack.x,
				p * Mathf.Sin (phi) * exp + 1 / layer.pos * posTrack.y
			);
			layer.obj.transform.localScale = 
				new Vector3 (exp, exp);
		}

		for (var i = layers.Count - 1; i >= 0; i--)
			if (layers [i].pos > 10) {
				Destroy (layers [i].obj);
				layers.RemoveAt (i);
			}
	}

}

class Layer
{
	public float pos = 0;
	public GameObject obj;

	public Layer (GameObject obj)
	{
		this.obj = obj;
	}

}

class H
{
	public static float Hypot (float x, float y)
	{
		return Mathf.Sqrt (x * x + y * y);
	}
}