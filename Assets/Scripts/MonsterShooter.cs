using UnityEngine;
using System.Collections;

public class MonsterShooter : Shooter {

	public override float charge { 
		get {return Player.charge;}
	}

	public override void Start() {
		base.Start ();
		foreach (var ball in ballsPool) {
			ball.name = "monster_ball";
		}
	}

	public override void Update () {
		if(Input.GetMouseButtonDown(0)){
			shoot();
		}
		if (charge > 0.01f) {
			loadWeapon();
		}
	}

	public override void loadWeapon() {
			if (ball)
				return;
			ball = ballsPool[currentBall];
			ball.name = "monster_ball";
			currentBall = currentBall == BALL_POOL_SIZE - 1 ? 0 : currentBall + 1;
			
			ball.transform.parent = transform;
			ball.transform.localPosition = new Vector3 (0.84f, 0.57f, 0);
			ball.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			ball.GetComponent<SpriteRenderer> ().color = Color.white;
		Player.charge -= 0.01f;
	}
}
