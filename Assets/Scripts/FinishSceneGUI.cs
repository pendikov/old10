using UnityEngine;
using System.Collections;

public class FinishSceneGUI : MonoBehaviour {

	public Font robotoRegular;
	private GUIStyle regularStyle;
	public Font robotoBlack;
	private GUIStyle blackStyle;

	public Texture2D restartButton;
	// Use this for initialization
	void Start () {
		blackStyle = new GUIStyle ();
		blackStyle.font = robotoRegular;
		blackStyle.normal.textColor = Color.white;
		blackStyle.fontSize = 60;
		blackStyle.alignment = TextAnchor.MiddleCenter;
		
		regularStyle = new GUIStyle ();
		regularStyle.font = robotoRegular;
		regularStyle.normal.textColor = Color.white;
		regularStyle.fontSize = 30;
		regularStyle.alignment = TextAnchor.MiddleCenter;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		float w = (float)Screen.width;
		//			float h = 60.0f;
		float h = (float)Screen.height;
		float x = (Screen.width - w) / 2.0f;
		float y = (Screen.height - h) / 2.0f - 75.0f;
		Rect rect = new Rect(x, y, w, h);
		
		GUI.Label(rect, "GAME OVER\nYOUR SCORE: "+Player.score, blackStyle);
		showRestart ();
	}

	private void showRestart() {
		float rbx = 30;
		float rby = 30;
		float rbw = restartButton.width;
		float rbh = restartButton.height;

		float x = ((float)Screen.width - rbw) / 2.0f;
		float y = (float)Screen.height / 2.0f + 10.0f;

		Rect rbrect = new Rect (x, y, rbw, rbh);
		
		Color color = GUI.color;
		
		if (rbrect.Contains (Event.current.mousePosition)) {
			color.a = 0.5f;
			if (Event.current.type == EventType.MouseUp) {
				GameManager.restart();
			}
		}
		GUI.color = color;
		GUI.DrawTexture (rbrect, restartButton);
		GUI.color = Color.white;
		
	}
}
