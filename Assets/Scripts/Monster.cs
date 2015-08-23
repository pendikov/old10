using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	public Object ballPrefab;
	public GameObject ball;
	private Camera cam;
	private int currentBall = 0;
	private const int BALL_POOL_SIZE = 10;
	GameObject[] ballsPool = new GameObject[BALL_POOL_SIZE];

	public bool canShoot {
		get {
			return ball != null;
		}
	}

	void Start () {
		ballPrefab = Resources.Load ("Prefabs/ball");
		cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		for (int i=0; i<BALL_POOL_SIZE; i++) {
			ballsPool[i] = Instantiate(ballPrefab) as GameObject;
			ballsPool[i].transform.localScale = Vector3.zero;
		}
		appear ();
	}

	void Update () {
		if(Input.GetMouseButtonDown(0)){
			shoot();
		}
		if (Player.charge > 0.01f) {
			loadWeapon();
		}
		if (Player.isDead) {
			disappear();
		}
	}

	void shoot(){
		if (canShoot)
			StartCoroutine("doShoot");
	}

	public IEnumerator doShoot() {
		const float time = 0.4f;
		float currentTime = 0.0f;
		Transform ballTransform = ball.transform;
		Vector3 firstPosition = ballTransform.position;

		GameObject tempBall = ball;
		ball = null;

		Vector3 secondPosition = cam.ScreenToWorldPoint (Input.mousePosition);
		while (currentTime <= time)
		{
			currentTime += Time.deltaTime;
			ballTransform.position = Vector3.Lerp(firstPosition, secondPosition, currentTime * 1.0f / time);
			float s = 1.0f - currentTime / time;
			ballTransform.localScale = new Vector3(s,s, 1);
			yield return null;
		}
		currentTime = 0.0f;

		yield return null;

	}

	public void loadWeapon() {
		if (ball)
			return;
		Player.charge -= 0.01f;

		ball = ballsPool[currentBall];
		currentBall = currentBall == BALL_POOL_SIZE - 1 ? 0 : currentBall + 1;

		ball.transform.parent = transform;
		ball.transform.localPosition = new Vector3 (0.84f, 0.57f, 0);
		ball.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
	}

	public void appear() {
		StartCoroutine ("doAppear");
	}

	public void disappear() {
		StartCoroutine ("doDisappear");
	}

	private IEnumerator doAppear() {

		const float time = 0.5f;
		float currentTime = 0.0f;

		Vector3 firstPosition = new Vector3 (0.0f, cam.ScreenToWorldPoint(new Vector3(0, -1)).y);		
		Vector3 secondPosition = new Vector3 (0, -2.4f);

		Transform thisTransform = transform;
		thisTransform.position = firstPosition;

		while (currentTime <= time)
		{
			currentTime += Time.deltaTime;
			thisTransform.position = Vector3.Lerp(firstPosition, secondPosition, currentTime / time);
			yield return null;
		}

		yield return null;
	}

	private IEnumerator doDisappear() {

		const float time = 0.5f;
		float currentTime = 0.0f;

		Vector3 firstPosition = transform.position;
		Vector3 secondPosition = new Vector3 (firstPosition.x, cam.ScreenToWorldPoint(new Vector3(0, -5)).y);		
		
		Transform thisTransform = transform;
		thisTransform.position = firstPosition;
		
		while (currentTime <= time)
		{
			currentTime += Time.deltaTime;
			thisTransform.position = Vector3.Lerp(firstPosition, secondPosition, currentTime / time);
			yield return null;
		}
		gameObject.GetComponent<SpriteRenderer> ().color = new Color (0.0f, 0.0f, 0.0f, 0.0f);
		yield return null;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if("Milonov_ball" == coll.collider.name) {
			getShot();
		}
	}

	void getShot() {
		StartCoroutine("doGetShot");
	}

	IEnumerator doGetShot() {

		SpriteRenderer ren = GetComponent<SpriteRenderer> ();
		ren.color = Color.red;

		yield return new WaitForSeconds (0.1f);

		ren.color = Color.white;

		Player.life -= 0.01f;

		yield return null;
	}
}
