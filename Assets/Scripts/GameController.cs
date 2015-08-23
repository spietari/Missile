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

	// This variable takes care of what happens when a game ends
	// or a new game is started.
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

	// This method takes care of starting the game and engaging / disengaging 
	// the autopilot.
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

	// A score is always given when autopilot is off.
	// Spawner gives progressively faster asteroids as the score increases.
	public void addKill() {
		if (!autopilot.enabled) {
			showMessage("+1");
			score.score++;
		}
		spawner.spawn(score.score);
	}

	// End the game whenever there's a miss.
	public void missed() {
		gameIsRunning = false;
	}

	// Display a new message and tween it upwards with a fade out.
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

}
