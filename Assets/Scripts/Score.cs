using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

	public Text scoreLabel;
	
	private float _score = 0;
	public float score {
		get {
			return _score;
		}

		set {
			_score = value;
			scoreLabel.text = "Score: " + _score;
		}	
	}

	public void reset() {
		score = 0;
	}
}
