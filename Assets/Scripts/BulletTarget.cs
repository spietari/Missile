using UnityEngine;
using System.Collections;

public class BulletTarget : MonoBehaviour {

	public GameObject bulletExplosionModel;
	public GameObject asteroidExplosionModel;

	private bool exploded = false;
	private GameController gameController;

	// Use this for initialization
	void Start () {
		gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		Debug.Log("OnTriggerEnter2D " + coll.gameObject.name + " " + exploded);

		if (coll.gameObject.tag == "bullet") {
			exploded = true;

			GameObject bulletExplosion = Instantiate(bulletExplosionModel, coll.gameObject.transform.position, Quaternion.identity) as GameObject;
			bulletExplosion.GetComponent<Rigidbody2D>().velocity = 0.2f * coll.gameObject.GetComponent<Rigidbody2D>().velocity;
			bulletExplosion.transform.parent = gameObject.transform;

			gameObject.GetComponent<CircleCollider2D>().radius = 0.1f;

			Destroy(coll.gameObject);
			Destroy(gameObject, 4);

		} else if (exploded && coll.gameObject.tag == "asteroid") {

			gameController.addKill();

			GameObject asteroidExplosion = Instantiate(asteroidExplosionModel, coll.gameObject.transform.position, Quaternion.identity) as GameObject;
			asteroidExplosion.GetComponent<Rigidbody2D>().velocity = 0.2f * coll.gameObject.GetComponent<Rigidbody2D>().velocity;

			Destroy(coll.gameObject);
			Destroy(gameObject, 4);

		}


	}

}
