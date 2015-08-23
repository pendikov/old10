using UnityEngine;
using System.Collections;

public class BonusItem : Sprite {

	public override float chargeBouns {
		get {
			return 0.01f;
		}
	}
	
	public override float lifeBouns {
		get {
			return 0.001f;
		}
	}

	void Start () {
	
	}

	void Update () {
	
	}
}
