using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	private GameController gameController;

	[HideInInspector]
	public bool autopilotShotTag = false;

	void Start () {
		gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
	}
		
	// If an asteroid gets too low, end the game.
	void Update () {
		if (transform.localPosition.y < 0) {
			gameController.missed();
			Destroy(gameObject);
		}
	}

	// When a bullet hits an asteroid just explode the bullet
	// and untag autopilot so that it can try shooting again.
	void OnCollisionEnter2D(Collision2D coll)  {
		if (coll.gameObject.tag == "bullet") {	
			Bullet bullet = coll.gameObject.GetComponent<Bullet>();
			bullet.explode();
			autopilotShotTag = false;
		}
	}
}
