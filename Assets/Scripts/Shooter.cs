using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
	public virtual Object ballPrefab { get; set;}
	public virtual GameObject ball { get; set;}
	public virtual Camera cam { get; set;}

	private int _currentBall = 0;
	public virtual int currentBall {
		get {return _currentBall;}
		set {_currentBall = value;}
	}

	public const int BALL_POOL_SIZE = 10;

	private GameObject[] _ballsPool = new GameObject[BALL_POOL_SIZE];
	public virtual GameObject[] ballsPool { 
		get{return _ballsPool;} 
		set {_ballsPool = value;}
	}

	private float _charge = 1.0f;
	public virtual float charge { 
		get {return _charge;}
	}
	
	public virtual bool canShoot {
		get {
			return ball != null;
		}
	}
	// Use this for initialization
	public virtual void Start () {
		ballPrefab = Resources.Load ("Prefabs/ball");
		cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		for (int i=0; i<BALL_POOL_SIZE; i++) {
			ballsPool[i] = Instantiate(ballPrefab) as GameObject;
			ballsPool[i].transform.localScale = Vector3.zero;
		}
	}
	
	// Update is called once per frame
	public virtual void Update () {

	}

	public virtual void shoot(){
		if (canShoot)
			StartCoroutine("doShoot");
	}
	
	public virtual IEnumerator doShoot() {
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
		tempBall.GetComponent<SpriteRenderer> ().color = Color.clear;
		
		yield return null;
		
	}
	
	public virtual void loadWeapon() {
		if (ball)
			return;
		
		ball = ballsPool[currentBall];
		currentBall = currentBall == BALL_POOL_SIZE - 1 ? 0 : currentBall + 1;
		
		ball.transform.parent = transform;
		ball.transform.localPosition = new Vector3 (0.84f, 0.57f, 0);
		ball.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		ball.GetComponent<SpriteRenderer> ().color = Color.white;
	}
}
