using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class Cannon : MonoBehaviour {

	public float minAngle = -120.0f;
	public float maxAngle =  120.0f;

	private const float fireSpeed = 120.0f;

	public GameObject pipe;

	public GameObject bulletModel;
	public GameObject bulletSpawnPoint;
	
	private float angle = 0;

	private GameController gameController;

	// Use this for initialization
	void Start () {
		gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (gameController.autopilot.enabled) return;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		aim(ray.origin);

		if (!gameController.gameIsRunning) return;

		if (Input.GetMouseButtonDown(0)) {
			fire(ray.origin);
		}
	}

	public bool aim(Vector2 position) {
		float x = pipe.transform.position.x - position.x;
		float y = position.y - pipe.transform.position.y;
		float newAngle = Mathf.Rad2Deg * Mathf.Atan2(x, y);
		newAngle = Mathf.Clamp(newAngle, minAngle, maxAngle);

		angle = Mathf.Lerp(angle, newAngle, 10.0f * Time.deltaTime);

		pipe.transform.localRotation = Quaternion.Euler(0, 0, angle);

		return Mathf.Abs(angle - newAngle) < 2.0;
	}

	public bool fire(Vector3 targetPosition) {

		if (Bullet.bulletsOnAir) {
			Debug.Log("There are bullets");
			return false;
		}

		GameObject bulletGO = GameObject.Instantiate(bulletModel, bulletSpawnPoint.transform.position, pipe.transform.rotation) as GameObject;
		Bullet bullet = bulletGO.GetComponent<Bullet>();
		Rigidbody2D rb = bulletGO.GetComponent<Rigidbody2D>();
		rb.AddRelativeForce(new Vector2(0, fireSpeed)); 
		bullet.setExplosionTarget(new Vector2(targetPosition.x, targetPosition.y));

		return true;
	}
}
