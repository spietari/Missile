using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class GameController : MonoBehaviour {

	private Score score;
	private AsteroidSpawner spawner;
	private Canvas canvas;

	public GameObject messageModel; 

	void Start() {
		score = GameObject.Find("Score").GetComponent<Score>();
		spawner = GameObject.Find("Asteroid Spawner").GetComponent<AsteroidSpawner>();
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		showMessage("Let's GO!");
	}

	public void addKill() {
		showMessage("+1");
		score.addScore();
		spawner.spawn();
	}

	public void missed() {
	
		showMessage("Game Over!", true);
	
	}

	public void showMessage(string message, bool permanent = false) {
	
		GameObject textGO = Instantiate(messageModel) as GameObject;
		textGO.transform.parent = canvas.transform;

		Text text = textGO.GetComponent<Text>();
		text.text = message;
		text.rectTransform.localPosition = new Vector3(0, 0, 0);

		if (!permanent) {
			text.CrossFadeAlpha(0, 3, true);
			text.rectTransform.localPositionTo(3, new Vector3(0, 100, 0), true);
			Destroy(textGO, 3);
		}
	}
}
