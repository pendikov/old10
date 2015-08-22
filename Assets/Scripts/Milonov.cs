using UnityEngine;
using System.Collections;

public class Milonov : Sprite
{

	public GameObject defaultFace;
	public GameObject star;

	private float starRotationSpeed = 2.0f;

	public override Color color {
		get {
			return star.GetComponent<SpriteRenderer> ().color;
		}
		set {
			star.GetComponent<SpriteRenderer> ().color = value;
			defaultFace.GetComponent<SpriteRenderer> ().color = value;
		}
	}

	public override int sortingOrder {
		get {
			return star.GetComponent<SpriteRenderer> ().sortingOrder;
		}
		set {
			star.GetComponent<SpriteRenderer> ().sortingOrder = value - 1;
			defaultFace.GetComponent<SpriteRenderer> ().sortingOrder = value;
		}
	}

	public override float chargeBouns {
		get {
			return 0.0f;
		}
	}
	
	public override float lifeBouns {
		get {
			return -0.01f;
		}
	}

	void Start ()
	{
	
	}

	void Update ()
	{
		star.transform.Rotate (new Vector3 (0, 0, starRotationSpeed));
	}

}
