using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public float spawnDistance = 8;

	public float asteroidSpeed = 50;

	private GameController gameController;



	// Use this for initialization
	void Start () {
	
		gameController = GameObject.Find("Game Controller").GetComponent<GameController>();

		float angle = Random.Range(Mathf.Deg2Rad * -45.0f, Mathf.Deg2Rad * 45.0f);

		transform.position = new Vector2(spawnDistance * Mathf.Sin(angle), spawnDistance * Mathf.Cos(angle));

		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		rb.angularVelocity = Random.Range(-100, 100);

		float speed = Random.value * asteroidSpeed;

		rb.AddRelativeForce(new Vector2(-speed * Mathf.Sin(angle), -speed * Mathf.Cos(angle)));

	}
		
	// Update is called once per frame
	void FixedUpdate () {
	
		if (transform.localPosition.y < 0) {
			gameController.missed();
			Destroy(gameObject);
		}

	}

	void OnCollisionEnter2D(Collision2D coll)  {
		if (coll.gameObject.tag == "bullet") {
			gameController.addKill();	
			Destroy(coll.gameObject);
			Destroy(gameObject);
		}
	}
}
