using UnityEngine;
using System.Collections;

public class MonsterShooter : Shooter {

	public override float charge { 
		get {return Player.charge;}
	}

	public override void Update () {
		if(Input.GetMouseButtonDown(0)){
			shoot();
		}
		if (charge > 0.01f) {
			loadWeapon();
		}
	}

	public virtual void loadWeapon() {

		if (ball)
			return;
		Player.charge -= 0.01f;
		base.loadWeapon ();
		ball.name = "Monster_ball";
	}
}
