using UnityEngine;
using System.Collections;

public class Sprite : MonoBehaviour {

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
