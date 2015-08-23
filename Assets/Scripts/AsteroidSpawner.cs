using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour {

	public GameObject asteroidModel;
	public float spawnDistance = 7.5f;
	public float asteroidMinSpeed = 50.0f;

	// This spawn an asteroid on an arc at spawnDistance from the cannot
	// +/- 45 degrees from the y axis. It's given a random angular velocity
	// and a random relative force pushes it downwards always towards the cannon.
	public void spawn(float level) {
		GameObject asteroidGO = Instantiate(asteroidModel);
		asteroidGO.transform.parent = gameObject.transform;

		float angle = Random.Range(Mathf.Deg2Rad * -45.0f, Mathf.Deg2Rad * 45.0f);
		asteroidGO.transform.position = new Vector2(spawnDistance * Mathf.Sin(angle), spawnDistance * Mathf.Cos(angle));

		Rigidbody2D rb = asteroidGO.GetComponent<Rigidbody2D>();
		rb.angularVelocity = Random.Range(-100, 100);
		float speed = Random.value * asteroidMinSpeed + level;
		rb.AddRelativeForce(new Vector2(-speed * Mathf.Sin(angle), -speed * Mathf.Cos(angle)));
	}
}
