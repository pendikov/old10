using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {

	public Texture2D textureLife;
	public Texture2D textureBattery;

	public Font robotoRegular;
	private GUIStyle myStyle;
	// Use this for initialization
	void Start () {
		myStyle = new GUIStyle ();
		myStyle.font = robotoRegular;
		myStyle.normal.textColor = Color.white;
		myStyle.fontSize = 30;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {myStyle.font = robotoRegular;

		Rect rect1 = new Rect(Screen.width - 100 - 50, 20, 100, 100);
		GUI.DrawTexture(rect1, textureBattery);

		GUI.Label(new Rect(Screen.width - 100 - 50,0,100,30),"300", myStyle);
	}
}
