using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {

	public Texture2D pinkHeart;
	public Texture2D redHeart;

	public Texture2D battery;
	public Texture2D yellow;

	public Texture2D restartButton;

	public Font robotoRegular;
	private GUIStyle regularStyle;
	public Font robotoBlack;
	private GUIStyle blackStyle;

	private GUIStyle scoreStyle;

	public Texture2D lifeFull;
	public Texture2D lifeEmpty;

	void Start () {

	}

	void OnGUI() {
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
		
		scoreStyle = new GUIStyle ();
		scoreStyle.font = robotoRegular;
		scoreStyle.normal.textColor = Color.white;
		scoreStyle.fontSize = 30;
		scoreStyle.alignment = TextAnchor.MiddleCenter;

		float batteryRightMargin = 20;
		float batteryWidth = battery.width / 2.0f;
		float batteryHeight = battery.height / 2.0f;
		float batteryX = Screen.width - batteryWidth - batteryRightMargin;
		float batteryY = 10;
		Rect batteryRect = new Rect (batteryX, batteryY, batteryWidth, batteryHeight);

		float yw = yellow.width / 2.0f;
		float yh = yellow.height / 2.0f;
		float yellowWidth = Player.charge * yw;
		float yellowHeight = yh;
		float yellowX = batteryX + 5.0f;
		float yellowY = batteryY + 5.0f;
		Rect yellowRect = new Rect (yellowX, yellowY, yellowWidth, yellowHeight);

		GUI.DrawTexture (yellowRect, yellow);
		GUI.DrawTexture (batteryRect, battery);

//		int numHearts = Player.MAX_LIVES;
//		float heartWidth = redHeart.width / 2.0f;
//		float heartHeight = redHeart.height / 2.0f;
//		float heartsRightMargin = 50 + batteryRightMargin + batteryWidth;
//		float spaceBetweenHearts = 10.0f;
//		float heartsY = 40.0f;
//
//		int livesLeft = Player.lives;
//		int livesLost = numHearts - livesLeft;
//
//		for (int i=0; i<livesLost; ++i) {
//			float x = Screen.width - heartsRightMargin - (heartWidth + spaceBetweenHearts) * (numHearts - i);
//			Rect rect = new Rect(x, heartsY, heartWidth, heartHeight);
//			GUI.DrawTexture(rect, pinkHeart);
//		}
//
//		for (int i=livesLost; i<numHearts; i++) {
//			float x = Screen.width - heartsRightMargin - (heartWidth + spaceBetweenHearts) * (numHearts - i);
//			Rect rect = new Rect(x, heartsY, heartWidth, heartHeight);
//			GUI.DrawTexture(rect, redHeart);
//		}
		float heartsRightMargin = 50 + batteryRightMargin + batteryWidth;
		float hw = lifeFull.width;
		float hh = lifeFull.height;
		float hx = Screen.width - heartsRightMargin - hw;
		float hy = 40.0f;
		Rect rectEmpty = new Rect (hx, hy, hw, hh);
		GUI.DrawTexture (rectEmpty, lifeEmpty);
		float hew = Player.life * hw;
		Rect rectFull = new Rect (hx, hy, hew, hh);
		GUI.DrawTextureWithTexCoords(rectFull,lifeFull,new Rect(0.0f, 0.0f, hew / hw, 1.0f));

		if (Player.isDead) {
			
			GameObject.Find("Main Camera").GetComponent<Main>().enabled = false;

			Application.LoadLevel(2);

//			float w = (float)Screen.width;
////			float h = 60.0f;
//			float h = (float)Screen.height;
//			float x = (Screen.width - w) / 2.0f;
//			float y = (Screen.height - h) / 2.0f;
//			Rect rect = new Rect(x, y, w, h);
//
//			GUI.Label(rect, "GAME OVER\nMOTHERFUCKER\nYOUR SCORE IS "+Player.score, blackStyle);
//
////			float w1 = (float)Screen.width;
////			float h1 = 60.0f;
////			float x1 = (Screen.width - w1) / 2.0f;
////			float y1 = y + h + 20;
////			Rect rect1 = new Rect(x1, y1, w1, h1);
////			
////			GUI.Label(rect1, "The faith is weak with this one", regularStyle);
//
			float rbx = 30;
			float rby = 30;
			float rbw = restartButton.width;
			float rbh = restartButton.height;
			Rect rbrect = new Rect (rbx, rby, rbw, rbh);
			
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
		//draw score
		float scoreW = 150.0f;
		float scoreH = 30.0f;
		float sx = hx - 30 - scoreW;
		float sy = 40.0f;
		Rect srect = new Rect(sx, sy, scoreW, scoreH);
		GUI.Label(srect, "SCORE: "+Player.score, scoreStyle);


	}

	private void showRestart() {}
}
