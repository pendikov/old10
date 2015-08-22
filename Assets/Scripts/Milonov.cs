using UnityEngine;
using System.Collections;

public class Milonov : MonoBehaviour
{

	public GameObject redEyesFace;
	public GameObject defaultFace;
	public GameObject star;

	private float starRotationSpeed = 2.0f;

	public Color color {
		get {
			return star.GetComponent<SpriteRenderer> ().color;
		}
		set {
			star.GetComponent<SpriteRenderer> ().color = value;
			redEyesFace.GetComponent<SpriteRenderer> ().color = value;
			defaultFace.GetComponent<SpriteRenderer> ().color = value;
		}
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
//		if (Input.GetKey (KeyCode.R)) {
//			defaultFace.SetActive (false);
//		} else {
//			defaultFace.SetActive(true);
//		}

		star.transform.Rotate (new Vector3 (0, 0, starRotationSpeed));
	}

}
