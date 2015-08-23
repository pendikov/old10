using UnityEngine;
using System.Collections;

public class Fish : Milonov {

	public override Color color {
		get {
			return GetComponent<SpriteRenderer> ().color;
		}
		set {
			GetComponent<SpriteRenderer> ().color = value;
		}
	}

	public override void Start () {
		base.Start ();
	}
	
	public override void Update () {
		transform.Rotate(new Vector3(0.0f, 0.0f, -200.0f * Time.deltaTime));

		if(isDead && !alreadyDead){
			dieHorribly();
			respawn();
		}
	}
}
