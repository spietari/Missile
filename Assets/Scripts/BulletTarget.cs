using UnityEngine;
using System.Collections;

public class BulletTarget : MonoBehaviour {

	public GameObject asteroidExplosionModel;

	private GameController gameController;

	void Start () {
		gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "asteroid") {

			gameController.addKill();

			GameObject asteroidGO = coll.gameObject;
			
			GameObject asteroidExplosion = Instantiate(asteroidExplosionModel, asteroidGO.transform.position - new Vector3(0, 0, 0.5f), asteroidGO.transform.rotation) as GameObject;
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
