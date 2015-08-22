using UnityEngine;
using System.Collections;

public class BonusItem : Sprite {

	public override float chargeBouns {
		get {
			return 0.001f;
		}
	}
	
	public override float lifeBouns {
		get {
			print ("lifebonus");
			return 0.001f;
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
