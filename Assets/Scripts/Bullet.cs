using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public GameObject bulletExplosionModel;
	public GameObject bulletTargetModel;

	static public bool bulletsOnAir = false;

	private Vector2 startPosition;
	private float explosionDistance = -1;

	// Static bulletsOnAir is always true when any bullet is created.
	void Start() {
		bulletsOnAir = true;
	}

	// Bullet explodes when it gets explosionDistance away from the starting location.
	void Update() {
		if (Vector3.Distance(startPosition, transform.position) > explosionDistance) {
			explode();
			createTarget();
		}
	}

	// This calculates the distance at which the bullet explodes based
	// on starting and target positions.
	public void setExplosionTarget(Vector2 target) {
		startPosition = transform.position;
		explosionDistance = Vector2.Distance(target, startPosition);
	}

	// Explosion resets the static variable so that another bullet can be shot.
	// An explosion particle system is created and the bullet game object is
	// destroyed.
	public void explode() {

		bulletsOnAir = false;

		GameObject bulletExplosion = Instantiate(bulletExplosionModel, transform.position, Quaternion.identity) as GameObject;
		bulletExplosion.GetComponent<Rigidbody2D>().velocity = 0.2f * GetComponent<Rigidbody2D>().velocity;
		Destroy(bulletExplosion, 4);

		Destroy(gameObject);
	}

	// A target is created when the bullet is exploded in free flight, i.e. when
	// it didn't hit an asteroid yet. The target created here destroyes the
	// asteroid on impact.
	private void createTarget() {
		GameObject bulletTarget = GameObject.Instantiate(bulletTargetModel, transform.position, Quaternion.identity) as GameObject;
		bulletTarget.GetComponent<Rigidbody2D>().velocity = 0.2f * GetComponent<Rigidbody2D>().velocity;
		Destroy(bulletTarget, 4);
	}
}
