using UnityEngine;
using System.Collections;

public class Player {

	public const int MAX_LIVES = 3;
	public const float MAX_CHARGE = 1.0f;
	public const float MAX_LIFE = 1.0f;


	public static int lives = MAX_LIVES;
	public static int score = 0;

	public static void reset() {
		score = 0;
		_life = 1.0f;
		_charge = 0.0f;
	}

	public static float life {
		get {
			return _life;
		}
		set {
			if (value <= 0.0f)
				_life = 0;
			else if (value <= MAX_LIFE)
				_life = value;
		}
	}
	private static float _life = 1.0f;

	public static float charge {
		get {
			return _charge;
		}
		set {
			if(value >= 0 && value <= MAX_CHARGE) 
				_charge = value;
		}
	}
	private static float _charge = 0;

	public static bool isDead {
		get {
			return _life <= 0.001f;
		}
	}

}
