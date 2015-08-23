using UnityEngine;
using System.Collections;

public class Sprite : MonoBehaviour {

	public virtual float chargeBouns {
		get {
			return Random.value * 0.01f;//0.0f;
		}
	}

	public virtual int scoreBonus {
		get {
			return 1;//0.0f;
		}
	}

	public virtual float lifeBouns {
		get {
			return - Random.value * 0.01f;//0.0f;
		}
	}

	public virtual int sortingOrder {
		get {
			return GetComponent<SpriteRenderer>().sortingOrder;
		}
		set {
			GetComponent<SpriteRenderer>().sortingOrder = value;
		}
	}
	public virtual Color color {
		get {
			return GetComponent<SpriteRenderer>().color;
		}
		set {
			GetComponent<SpriteRenderer>().color = value;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
