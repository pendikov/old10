using UnityEngine;
using System.Collections;

public class Milonov : Sprite
{
	public const float MAX_HP = 1.0f;

	public GameObject defaultFace;
	public GameObject star;

	public float starRotationSpeed = 200.0f;

	public float hp = 0.0f;

	public Monster monster;

	public bool isDead { get {
			return hp <= 0.001f;
		}}

//	public bool shouldDie {
//		get {
//			return hp <= 0.001f;
//		}
//	}
	private bool alreadyDead = false;

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
		color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
	}
	
	void Update ()
	{
		star.transform.Rotate (new Vector3 (0, 0, starRotationSpeed*Time.deltaTime));
		if (Input.GetKey (KeyCode.H)) {
			dieHorribly();
		}
		if (Input.GetKey (KeyCode.V)) {
			show ();
		}
		if(isDead && !alreadyDead){
			dieHorribly();
		}
		print (isDead);
		if (color.a <= 0.001f) {
			print (1);
			if(Random.value > 0.8f){
				print(2);
				respawn();
			}
		}
	}

	public void show() {
//		color = Color.white;
		StartCoroutine ("doShow");
	}

	IEnumerator doShow() {
		color = Color.white;
		transform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
		float time = 0.3f;
		float ct = 0.0f;
		
		while (ct < time) {
			ct+= Time.deltaTime;
			color = Color.Lerp(new Color(1.0f, 1.0f, 1.0f, 0.0f), Color.white, ct / time);
			yield return null;
		}
	}

	public void hide() {
//		color = new Color (0f, 0f, 0f, 0f);
		StartCoroutine ("doHide");
	}

	IEnumerator doHide() {
		float time = 0.3f;
		float ct = 0.0f;
		
		while (ct < time) {
			ct+= Time.deltaTime;
			color = Color.Lerp(Color.white, new Color(1.0f, 1.0f, 1.0f, 0.0f), ct / time);
			yield return null;
		}
	}

	public void respawn() {
		hp = MAX_HP;
		alreadyDead = false;
		moveRandomly ();
	}

	public void hurt() {
		if (monster.canShoot) {
			StartCoroutine ("doHurt");
			hp -= Random.value / 3.0f;
		}
	}

	public void dieHorribly() {
		if(color.a <= 0.001) {
			return;
		}
		alreadyDead = true;
		StartCoroutine ("doDieHorribly");
	}

	IEnumerator doDieHorribly() {
		color = Color.red;
		GetComponent<ParticleSystem> ().Play ();
		yield return new WaitForSeconds (GetComponent<ParticleSystem>().duration);

		StopCoroutine ("doMoveRandomly");

		float randomTime = 0.3f;
		float currentTime = 0.0f;

		Vector3 currentScale = transform.localScale;
		Vector3 newScale = Vector3.zero;
		
		while (currentTime < randomTime) {
			currentTime += Time.deltaTime;			
			transform.localScale = Vector3.Lerp(currentScale, newScale, currentTime/randomTime);
			
			yield return null;
		}

		hide ();
	}

	private IEnumerator doHurt() {
		color = Color.red;
		yield return new WaitForSeconds (0.1f);
		color = Color.white;
	}


	void moveRandomly() {
		StartCoroutine ("doMoveRandomly");
	}

	IEnumerator doMoveRandomly() {
		color = Color.white;
		transform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
		while (true) {

			Vector3 startPosition = transform.localPosition;
			Vector3 randomPoint = H.RandomPointInCircle (5.0f);
			float randomTime = Random.value +0.3f;
			float currentTime = 0.0f;

			float rand = (Random.value + 0.14f);
			float randomScale = rand > 0.65f ? 0.65f : rand;
			Vector3 currentScale = transform.localScale;
			Vector3 newScale = new Vector3(1.0f, 1.0f, 1.0f) * randomScale;

			while (currentTime < randomTime) {
				currentTime += Time.deltaTime;
				transform.localPosition = Vector3.Lerp (startPosition, randomPoint, currentTime/randomTime);

				transform.localScale = Vector3.Lerp(currentScale, newScale, currentTime/randomTime);

				yield return null;
			}
			yield return new WaitForSeconds(Random.value );

		}
	}

	void OnMouseDown() {
		hurt ();
	}

}
