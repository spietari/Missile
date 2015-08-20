using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class Cannon : MonoBehaviour {

	public float minAngle = -120.0f;
	public float maxAngle =  120.0f;
	public float maxSpeed = 300.0f;

	public GameObject pipe;

	public GameObject bulletModel;
	public GameObject bulletSpawnPoint;
	public GameObject bulletTargetModel;

	private float angle = 0;

	private GameController gameController;
	
	// Use this for initialization
	void Start () {
		gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (!gameController.gameIsRunning) return;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		float x = pipe.transform.position.x - ray.origin.x;
		float y = ray.origin.y - pipe.transform.position.y;

		angle = Mathf.Rad2Deg * Mathf.Atan2(x, y) + 0;

		angle = Mathf.Clamp(angle, minAngle, maxAngle);
	
		pipe.transform.localRotation = Quaternion.Euler(0, 0, angle);

		if (Input.GetMouseButtonDown(0)) {
			fire(300, ray.origin);
		}
	}

	void fire(float speed, Vector3 targetPosition) {

		if (GameObject.FindGameObjectsWithTag("bullet").Length > 0) {
			return;
		}

		GameObject bullet = GameObject.Instantiate(bulletModel, bulletSpawnPoint.transform.position, pipe.transform.rotation) as GameObject;
		GameObject target = GameObject.Instantiate(bulletTargetModel, targetPosition, Quaternion.identity) as GameObject;
		
		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
		rb.AddRelativeForce(new Vector2(0, speed)); 
		
		Destroy(bullet, 2);
	}
}
