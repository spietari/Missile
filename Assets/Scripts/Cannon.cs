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

	public Image speedBar;

	private float angle = 0;

	private float speed = 0;

	// Use this for initialization
	void Start () {
		speedBar.fillAmount = 0;
		speedBar.SetAllDirty();	
	}
	
	// Update is called once per frame
	void Update () {
	
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.Log(ray.origin.x + " " + ray.origin.y);

		float x = pipe.transform.position.x - ray.origin.x;
		float y = ray.origin.y - pipe.transform.position.y;

		angle = Mathf.Rad2Deg * Mathf.Atan2(x, y) + 0;

		angle = Mathf.Clamp(angle, minAngle, maxAngle);
	
		pipe.transform.localRotation = Quaternion.Euler(0, 0, angle);

		if (Input.GetMouseButton(0)) {
			speed += 10;
			speed = Mathf.Clamp(speed, 0, 300);
			speedBar.fillAmount = speed / maxSpeed;
			speedBar.SetAllDirty();	
		}

		if (speed > 0 && Input.GetMouseButtonUp(0)) {

			GameObject bullet = GameObject.Instantiate(bulletModel, bulletSpawnPoint.transform.position, pipe.transform.rotation) as GameObject;
			
			Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
			rb.AddRelativeForce(new Vector2(0, speed)); 
			
			Destroy(bullet, 2);

			speed = 0;
			speedBar.fillAmount = 0;
			speedBar.SetAllDirty();	
		}
	}
}
