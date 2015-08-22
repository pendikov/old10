using UnityEngine;
using System.Collections;

public class GameManager {

	public static void startGame () {

		Application.LoadLevel ("Scene");

		Player.charge = 1.0f;
		Player.lives = Player.MAX_LIVES;
		Player.score = 0;
	}

	public static void restart() {
		Application.LoadLevel ("Start Scene");
	}
}
