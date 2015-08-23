using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public GameObject bulletExplosionModel;
	public GameObject bulletTargetModel;

	static public bool bulletsOnAir = false;

	private Vector2 startPosition;
	private float explosionDistance = -1;

	void Start() {
		bulletsOnAir = true;
		StartCoroutine(autoDestruct());
	}

	void Update() {

		Debug.Log(Vector3.Distance(startPosition, transform.position) + " " + explosionDistance);

		if (Vector3.Distance(startPosition, transform.position) > explosionDistance) {
			explode();
			createTarget();
		}
	}

	IEnumerator autoDestruct() {
		yield return new WaitForSeconds(2);
		explode();
	}

	public void setExplosionTarget(Vector2 target) {
		startPosition = transform.position;
		explosionDistance = Vector2.Distance(target, startPosition);
	}

	public void explode() {

		Debug.Log("explode");

		bulletsOnAir = false;

		GameObject bulletExplosion = Instantiate(bulletExplosionModel, transform.position, Quaternion.identity) as GameObject;
		bulletExplosion.GetComponent<Rigidbody2D>().velocity = 0.2f * GetComponent<Rigidbody2D>().velocity;
		Destroy(bulletExplosion, 4);

		Destroy(gameObject);
	}

	private void createTarget() {
		GameObject bulletTarget = GameObject.Instantiate(bulletTargetModel, transform.position, Quaternion.identity) as GameObject;
		Destroy(bulletTarget, 4);

	}
}
