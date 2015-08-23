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
			return 0.001f;
		}
	}

	public float timeDecrement {
		get {
			return 5;
		}
	}

	public void pickup() {
		Player.score += 1;
	}

	void Start () {
	
	}

	void Update () {
	
	}
}
