using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {

	public Texture2D pinkHeart;
	public Texture2D redHeart;

	public Texture2D battery;
	public Texture2D yellow;

	public Font robotoRegular;
	private GUIStyle myStyle;

	void Start () {
		myStyle = new GUIStyle ();
		myStyle.font = robotoRegular;
		myStyle.normal.textColor = Color.white;
		myStyle.fontSize = 30;
	}

	void OnGUI() {
		float batteryRightMargin = 20;
		float batteryWidth = battery.width;
		float batteryHeight = battery.height;
		float batteryX = Screen.width - battery.width - batteryRightMargin;
		float batteryY = 10;
		Rect batteryRect = new Rect (batteryX, batteryY, batteryWidth, batteryHeight);

		float yellowWidth = Player.charge * yellow.width;
		float yellowHeight = yellow.height;
		float yellowX = batteryX + 5.0f;
		float yellowY = batteryY + 5.0f;
		Rect yellowRect = new Rect (yellowX, yellowY, yellowWidth, yellowHeight);

		GUI.DrawTexture (yellowRect, yellow);
		GUI.DrawTexture (batteryRect, battery);

		int numHearts = Player.MAX_LIVES;
		float heartWidth = redHeart.width;
		float heartHeight = redHeart.height;
		float heartsRightMargin = 50 + batteryRightMargin + batteryWidth;
		float spaceBetweenHearts = 10.0f;
		float heartsY = 40.0f;

		int livesLeft = Player.lives;
		int livesLost = numHearts - livesLeft;

		for (int i=0; i<livesLost; ++i) {
			float x = Screen.width - heartsRightMargin - (heartWidth + spaceBetweenHearts) * (numHearts - i);
			Rect rect = new Rect(x, heartsY, heartWidth, heartHeight);
			GUI.DrawTexture(rect, pinkHeart);
		}

		for (int i=livesLost; i<numHearts; i++) {
			float x = Screen.width - heartsRightMargin - (heartWidth + spaceBetweenHearts) * (numHearts - i);
			Rect rect = new Rect(x, heartsY, heartWidth, heartHeight);
			GUI.DrawTexture(rect, redHeart);
		}



	}
}
