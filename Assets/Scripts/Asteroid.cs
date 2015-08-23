using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	private GameController gameController;

	[HideInInspector]
	public bool autopilotShotTag = false;

	void Start () {
		gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
	}
		
	void FixedUpdate () {
		if (transform.localPosition.y < 0) {
			gameController.missed();
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll)  {
		if (coll.gameObject.tag == "bullet") {
//			gameController.addKill();	
			Bullet bullet = coll.gameObject.GetComponent<Bullet>();
			bullet.explode();
			autopilotShotTag = false;
//			Destroy(gameObject);
		}
	}
}
