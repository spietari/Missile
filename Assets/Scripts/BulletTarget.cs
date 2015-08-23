using UnityEngine;
using System.Collections;

public class BulletTarget : MonoBehaviour {

	public GameObject asteroidExplosionModel;

	private GameController gameController;

	void Start () {
		gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
	}

	// This trigger is run when the bullet target and asteroid collider
	// hit. This is the "Stay" form of this method because sometimes 
	// the bullet explodes already inside the collider and enter would
	// have already been called and the asteroid would miss the bullet.
	// An explosion is created here and game controller is notified of
	// a successful hit.
	// The asteroid explosion inherits some of the asteroid velocity and
	// angular velocity.
	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "asteroid") {

			gameController.addKill();

			GameObject asteroidGO = coll.gameObject;

			Vector3 explosionPoint = (transform.position + asteroidGO.transform.position) / 2.0f;

			// Move the explosion on the top of the asteroid in the view hierarchy, hence the 0.5f z vector.
			GameObject asteroidExplosion = Instantiate(asteroidExplosionModel, explosionPoint - new Vector3(0, 0, 0.5f), asteroidGO.transform.rotation) as GameObject;
			Rigidbody2D asteroidRB = asteroidGO.GetComponent<Rigidbody2D>();
			Rigidbody2D explosionRB = asteroidExplosion.GetComponent<Rigidbody2D>();

			explosionRB.velocity = 1.0f * asteroidRB.velocity;
			asteroidRB.velocity = explosionRB.velocity;
			explosionRB.angularVelocity = 0.4f * asteroidRB.angularVelocity;
			
			Destroy(asteroidGO, 0.75f);
			Destroy(gameObject);
			Destroy(asteroidExplosion, 4);
		}
	}
}
