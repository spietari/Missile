using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class GameController : MonoBehaviour {

	private Score score;
	private AsteroidSpawner spawner;
	private Canvas canvas;
	public GameObject messageModel; 


	[HideInInspector]
	public Autopilot autopilot;

	private Text welcomeText;
	private bool _gameIsRunning = false;

	public bool gameIsRunning {
	
		get {
			return _gameIsRunning;
		}
	
		set {
			if (_gameIsRunning == value) return;
			if (value) {
				welcomeText.CrossFadeAlpha(0.0f, 1.0f, true);
				score.reset();
				spawner.spawn(0);
			} else {
				showMessage("Game Over! Click anywhere to try again!");
			}
			_gameIsRunning = value;
		}
	}
	
	void Start() {
		
		Go.defaultEaseType = GoEaseType.CircIn;
		
		score = GameObject.Find("Score").GetComponent<Score>();
		spawner = GameObject.Find("Asteroid Spawner").GetComponent<AsteroidSpawner>();
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		autopilot = GameObject.Find("Autopilot").GetComponent<Autopilot>();
		welcomeText = GameObject.Find("WelcomeText").GetComponent<Text>();

		autopilot.enabled = false;
	}

	void Update() {
		if (!gameIsRunning && Input.GetMouseButtonDown(0)) {
			gameIsRunning = true;
		}

		if (gameIsRunning && Input.GetMouseButtonDown(1)) {
			if (!autopilot.enabled) {
				autopilot.enabled = true;
				showMessage("Autopilot engaged!");
			} else {
				autopilot.enabled = false;
				showMessage("Autopilot disconnected!");
			}
		}
	}
	
	public void addKill() {
		if (!autopilot.enabled) {
			showMessage("+1");
			score.score++;
		}
		spawner.spawn(score.score);
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
		text.rectTransform.localPositionTo(duration, new Vector3(0, duration * 100, 0), true);
		Destroy(textGO, duration);

	}
	
	public void startGame() {
		score.reset();  
		spawner.spawn(0);
	}
}
