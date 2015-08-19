using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public float spawnDistance = 8;

	private GameController gameController;

	// Use this for initialization
	void Start () {
	
		gameController = GameObject.Find("Game Controller").GetComponent<GameController>();

		float angle = Random.Range(Mathf.Deg2Rad * -45.0f, Mathf.Deg2Rad * 45.0f);

		transform.position = new Vector2(spawnDistance * Mathf.Sin(angle), spawnDistance * Mathf.Cos(angle));

		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		rb.angularVelocity = Random.Range(-100, 100);

		rb.AddRelativeForce(new Vector2(Random.Range(-50, 50), Random.Range(-100, 0)));

	}
		
	// Update is called once per frame
	void FixedUpdate () {
	
		if (transform.localPosition.y < 0) {
			gameController.missed();
			Destroy(gameObject);
		}

	}

	void OnCollisionEnter2D(Collision2D coll)  {
		gameController.addKill();
		Destroy(coll.gameObject);
		Destroy(gameObject);
	}
}
