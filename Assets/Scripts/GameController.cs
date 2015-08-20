using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class GameController : MonoBehaviour {

	private Score score;
	private AsteroidSpawner spawner;
	private Canvas canvas;

	public GameObject messageModel; 
		
	private bool _gameIsRunning = false;

	public bool gameIsRunning {
	
		get {
			return _gameIsRunning;
		}
	
		set {
			if (_gameIsRunning == value) return;
			if (value) {
				score.reset();
				showMessage("Destroy the asteroids!");
				spawner.spawn();
			} else {
				showMessage("Game Over! Click anywhere to try again!");
			}
			_gameIsRunning = value;
		}
	}

	void Update() {
		if (!gameIsRunning && Input.GetMouseButtonDown(0)) {
			gameIsRunning = true;
		}
	}

	void Start() {
		score = GameObject.Find("Score").GetComponent<Score>();
		spawner = GameObject.Find("Asteroid Spawner").GetComponent<AsteroidSpawner>();
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		showMessage("Let's GO! Click anywhere to start playing!", 10);
	}

	public void addKill() {
		showMessage("+1");
		score.score++;
		spawner.spawn();
	}

	public void missed() {
		gameIsRunning = false;
	}

	public void showMessage(string message, float duration = 3.0f) {
	
		GameObject textGO = Instantiate(messageModel) as GameObject;
		textGO.transform.SetParent(canvas.transform);

		Text text = textGO.GetComponent<Text>();
		text.text = message;
		text.rectTransform.localPosition = new Vector3(0, 0, 0);

		text.CrossFadeAlpha(0, duration, true);
		text.rectTransform.localPositionTo(duration, new Vector3(0, duration * 100 / 3.0f, 0), true);
		Destroy(textGO, duration);

	}
	
	public void startGame() {
		score.reset();  
		spawner.spawn();
	}
}
