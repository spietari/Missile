using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour {

	public GameObject asteroidModel;
	public GameObject planet;

	// Use this for initialization
	void Start () {
		spawn();
	}

	public void spawn() {
		GameObject asteroid = Instantiate(asteroidModel);
		asteroid.transform.parent = gameObject.transform;
	}
}
