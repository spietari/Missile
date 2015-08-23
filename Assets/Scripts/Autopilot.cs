﻿using UnityEngine;
using System.Collections;

public class Autopilot : MonoBehaviour {

	private Cannon cannon;

	// Use this for initialization
	void Start () {
		cannon = GameObject.Find("Cannon").GetComponent<Cannon>();
	}
	
	// Update is called once per frame
	void Update () {
	
		GameObject[] asteroids = GameObject.FindGameObjectsWithTag("asteroid");

		foreach (GameObject asteroidGO in asteroids) {
		
			Debug.Log("Asteroid " + asteroidGO.name);

			Asteroid asteroid = asteroidGO.GetComponent<Asteroid>();

			if (asteroid == null) {
				Debug.Log("Asteroid null");
			}

			if (asteroid.autopilotShotTag) continue;

			Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();

			Vector2 asteroidPosition = new Vector2(asteroid.transform.position.x, asteroid.transform.position.y);
			Vector2 aimOffset = 2.0f * rb.velocity;

			aimOffset = 4.0f * aimOffset.normalized;

			Vector2 aimPoint = asteroidPosition + aimOffset;

			if (aimPoint.y < cannon.transform.position.y) {
				aimPoint = asteroidPosition + rb.velocity;
			}

			if (cannon.aim(aimPoint) && asteroidPosition.y < 5) {
				asteroid.autopilotShotTag = cannon.fire(aimPoint);
			}

		}

	}
}
