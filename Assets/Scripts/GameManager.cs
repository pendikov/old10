using UnityEngine;
using System.Collections;

public class GameManager {

	public static void startGame () {

		Application.LoadLevel ("Scene");
		Player.reset ();
	}

	public static void restart() {
		Application.LoadLevel ("Start Scene");
	}

}
