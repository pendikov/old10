using UnityEngine;
using System.Collections;

public class MilonovShooter : Shooter {

	public Monster monster;

	public override void Start () {
		base.Start ();
	}

	public void shootPlayer() {
		StartCoroutine ("doShootPlayer");
	}

	private IEnumerator doShootPlayer() {
//		yield return new WaitForSeconds(Random.value * 2.0f);
		loadWeapon();
		shoot();
		yield return null;
	}

	public override IEnumerator doShoot() {
		const float time = 0.4f;
		float currentTime = 0.0f;
		Transform ballTransform = ball.transform;
		Vector3 firstPosition = ballTransform.position;
		Vector3 secondPosition = monster.transform.position;

		ballTransform.localScale = Vector3.zero;
		
		GameObject tempBall = ball;
		ball = null;
		
		while (currentTime <= time)
		{
			currentTime += Time.deltaTime;
			ballTransform.position = Vector3.Lerp(firstPosition, secondPosition, currentTime * 1.0f / time);
			float s = currentTime / time;
			ballTransform.localScale = new Vector3(s,s, 1);
			yield return null;
		}
		tempBall.GetComponent<SpriteRenderer> ().color = Color.clear;
		
		yield return null;
		
	}
	
	public override void loadWeapon() {
		if (ball)
			return;
		
		ball = ballsPool[currentBall];
		currentBall = currentBall == BALL_POOL_SIZE - 1 ? 0 : currentBall + 1;
		
		ball.transform.parent = transform;
		ball.transform.localPosition = new Vector3 (0f, 0.0f, 0);
		ball.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		ball.GetComponent<SpriteRenderer> ().color = Color.white;
	}




}
