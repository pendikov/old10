using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
	UnityEngine.Object circlePrefab;
	UnityEngine.Object milonovPrefab;
	UnityEngine.Object monsterPrefab;

	const float TRACK_SPEED = .01f;

	Dir monsterSpeed = new Dir ();
	Layer monsterLayer;
	Item monster;
	List<Layer> layers = new List<Layer> ();

	Vector3 track1 = new Vector3 (0, 0);
	Vector3 track2 = new Vector3 (0, 0);
	float trackTime = 0;

	float tunnelSpeed = .001f;

	void Start ()
	{
		circlePrefab = Resources.Load ("Prefabs/Oval4");
		milonovPrefab = Resources.Load ("Prefabs/Milonov2");
		monsterPrefab = Resources.Load ("Prefabs/monstr@2x");

		monsterLayer = new Layer (new GameObject ());
		monsterLayer.pos = .95f;
		monster = new Item ();
		monster.obj = Instantiate (monsterPrefab) as GameObject;
		monsterLayer.items.Add (monster);
		monster.obj.transform.parent = monsterLayer.obj.transform;
	}

	void Update ()
	{
		tunnelSpeed += .000005f;
//		if (tunnelSpeed >= .015)
//			GameObject.Find ("Main Camera").GetComponent<Camera> ().clearFlags = CameraClearFlags.Depth;

		trackTime += 10 * Mathf.Sqrt (tunnelSpeed) * TRACK_SPEED;
		while (trackTime >= 1) {
			trackTime -= 1;
			track1 = track2;
			track2 = H.RandomPointInCircle (1);
		}

		Vector3 posTrack = 
			track1 * Mathf.Cos (trackTime * Mathf.PI / 2) +
			track2 * (1 - Mathf.Cos (trackTime * Mathf.PI / 2));

//		float p = 10 * Mathf.Min (.5f, Mathf.Sqrt (
//			          Mathf.Pow (Input.mousePosition.x / Screen.width - .5f, 2) +
//			          Mathf.Pow (Input.mousePosition.y / Screen.height - .5f, 2)));
//		float phi = 
//			Mathf.Atan2 (Input.mousePosition.y / Screen.height - .5f, Input.mousePosition.x / Screen.width - .5f);

		{
			float dx = (Input.GetKey (KeyCode.D) ? 1 : 0) - (Input.GetKey (KeyCode.A) ? 1 : 0);
			float dy = (Input.GetKey (KeyCode.W) ? 1 : 0) - (Input.GetKey (KeyCode.S) ? 1 : 0);
			monsterSpeed.p = Mathf.Max (0, monsterSpeed.p - .05f);
			if (dx != 0 || dy != 0) {
				monsterSpeed.x += .1f * dx;
				monsterSpeed.y += .1f * dy;
				monsterSpeed.p = Mathf.Min (.3f, monsterSpeed.p);
			}
			monster.obj.transform.localPosition += new Vector3 (monsterSpeed.x, monsterSpeed.y);
			float l = H.Hypot (monster.obj.transform.localPosition);
			if (l > 5)
				monster.obj.transform.localPosition *= 5f / l;
		}
		float p = -H.Hypot (monster.obj.transform.localPosition.x, monster.obj.transform.localPosition.y) / 2;
		float phi = Mathf.Atan2 (monster.obj.transform.localPosition.y, monster.obj.transform.localPosition.x);
		{
			var exp = .01f * Mathf.Pow (1.05f, 100 * monsterLayer.pos);
			monsterLayer.obj.transform.localPosition = new Vector3 (
				p * Mathf.Cos (phi) * exp + 1 / monsterLayer.pos * posTrack.x,
				p * Mathf.Sin (phi) * exp + 1 / monsterLayer.pos * posTrack.y
			);
			monsterLayer.obj.transform.localScale = new Vector3 (exp, exp);
		}

		if (Time.frameCount % 8 == 0) {
			Layer layer = new Layer (Instantiate (circlePrefab) as GameObject);
			layers.Add (layer);
			layer.obj.GetComponent<SpriteRenderer> ().color = new Color (
				Random.value,
				Random.value,
				Random.value
			);

			if (Random.value < .3f) {
				Item item = new Item ();
				item.obj = Instantiate (milonovPrefab) as GameObject;
				item.obj.transform.SetParent (layer.obj.transform);
				item.obj.transform.localPosition = H.RandomPointInCircle (5);
				item.obj.GetComponent<Milonov> ().sortingOrder = -Time.frameCount;
				layer.items.Add (item);
			}
		}
		foreach (var layer in layers) {
			layer.pos += tunnelSpeed;
			var exp = .01f * Mathf.Pow (1.05f, 100 * layer.pos);
			layer.obj.transform.localPosition = new Vector3 (
				p * Mathf.Cos (phi) * exp + 1 / layer.pos * posTrack.x,
				p * Mathf.Sin (phi) * exp + 1 / layer.pos * posTrack.y
			);
			layer.obj.transform.localScale = new Vector3 (exp, exp);
			foreach (var item in layer.items) {
				if (layer.pos >= .9 && layer.pos < 1) {
					if (H.Hypot (monster.obj.transform.localPosition, item.obj.transform.localPosition)
					    < monster.radius + item.radius) {
						item.obj.GetComponent<Milonov> ().color = new Color (1, 0, 0, 1);
					}
				}
				if (layer.pos > 1)
					item.obj.GetComponent<Milonov> ().color = new Color (
						item.obj.GetComponent<Milonov> ().color.r,
						item.obj.GetComponent<Milonov> ().color.g,
						item.obj.GetComponent<Milonov> ().color.b,
						Mathf.Min (1, Mathf.Max (0, 6.5f - 6 * layer.pos))
					);
//				else
//					item.obj.GetComponent<Milonov> ().color = new Color (
//						item.obj.GetComponent<Milonov> ().color.r,
//						item.obj.GetComponent<Milonov> ().color.g,
//						item.obj.GetComponent<Milonov> ().color.b,
//						Mathf.Min (1, Mathf.Max (0, 6.5f - 6 * layer.pos))
//					);
				// тут продолжаем
			}
		}

		for (var i = layers.Count - 1; i >= 0; i--)
			if (layers [i].pos > 3) {
				Destroy (layers [i].obj);
				layers.RemoveAt (i);
			}
	}

}

class Item
{
	public float radius = 1;
	public GameObject obj;
}

class Layer
{
	public float pos = 0;
	public GameObject obj;
	public List<Item> items = new List<Item> ();

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

	public static float Hypot (Vector3 f, Vector3 s)
	{
		return Mathf.Sqrt ((f.x - s.x) * (f.x - s.x) + (f.y - s.y) * (f.y - s.y));
	}

	public static float Hypot (Vector3 v)
	{
		return Mathf.Sqrt (v.x * v.x + v.y * v.y);
	}

	public static Vector3 RandomPointInCircle (float r)
	{
		Vector3 point;
		do {
			point = new Vector3 (
				2 * r * (Random.value - .5f),
				2 * r * (Random.value - .5f)
			);
		} while(H.Hypot (point.x, point.y) > r);
		return point;
	}

}

class Dir
{
	public float x = 0;
	public float y = 0;

	public float p {
		get {
			return H.Hypot (x, y);
		}
		set {
			float _phi = phi;
			x = value * Mathf.Cos (phi);
			y = value * Mathf.Sin (phi);
		}
	}

	public float phi {
		get {
			return Mathf.Atan2 (y, x);
		}
		set {
			float _p = p;
			x = _p * Mathf.Cos (value);
			y = _p * Mathf.Sin (value);
		}
	}

}
