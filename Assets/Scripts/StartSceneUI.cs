using UnityEngine;
using System.Collections;

public class StartSceneUI : MonoBehaviour {

	public Texture2D startButton;
	public Texture2D circlesTexture;

	void Start() {


	}

	void OnGUI () {
		GUI.skin.button.normal.background = startButton;
		GUI.skin.button.hover.background = startButton;

		float cx = (Screen.width - circlesTexture.width) / 2.0f;
		float cy = (Screen.height - circlesTexture.height) / 2.0f;
		Rect circlesRect = new Rect(cx, cy, circlesTexture.width, circlesTexture.height);
		GUI.DrawTexture (circlesRect, circlesTexture);

		float sbw = 189;
		float sbh = 135;

		float sbx = (Screen.width - sbw) / 2.0f+6;
		float sby = (Screen.height - sbh) / 2.0f;
		Rect sbRect = new Rect (sbx, sby, sbw, sbh);

		Event e = Event.current;

		if (sbRect.Contains (e.mousePosition)) {
			GUI.color = new Color(1f, 1f, 1f, 0.5f);
			if(e.type == EventType.MouseUp ) {
				print ("click");
				startGameButtonPressed();
			}
		} 
		GUI.DrawTexture (sbRect, startButton);
	}

	void startGameButtonPressed() {

		GameManager.startGame ();

	}
}
