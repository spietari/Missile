using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

	public Text scoreLabel;

	//private float score = 0;

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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void reset() {
		score = 0;
	}
}
