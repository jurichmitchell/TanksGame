using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public bool debugOn;

	// Prefab references
	public GameObject standardBullet;

	// Children references
	private GameObject cannon;
	private Rigidbody rb;

	// Member variables
	private List<GameObject> currentCollisions = new List<GameObject>();	// Stores a list of all objects this is currently colliding with

	private float rotateSpeed = 50.0f;
	private float moveSpeed = 1.0f;
	private bool isRagdoll = false;

    // Start is called before the first frame update
    void Start() {
		cannon = this.transform.Find("Cannon").gameObject;
		rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
		FireProjectile();
    }

	// For physics calculations
	void FixedUpdate() {
		MovePlayer();
	}

	private void OnCollisionEnter(Collision collision) {
		currentCollisions.Add(collision.gameObject);
	}

	private void OnCollisionExit(Collision collision) {
		currentCollisions.Remove(collision.gameObject);
	}
	
	private void MovePlayer() {
		float horComp = Input.GetAxis("Horizontal");
		float vertComp = Input.GetAxis("Vertical");
		Vector3 currPos = this.gameObject.transform.position;
		Vector3 forwardMovement = gameObject.transform.forward * (vertComp * moveSpeed);

		// Update player's position
		rb.MovePosition(currPos + (forwardMovement * Time.fixedDeltaTime));
		// Update player's rotation
		rb.MoveRotation(rb.rotation * Quaternion.Euler(0.0f, horComp * rotateSpeed * Time.fixedDeltaTime, 0.0f));

		// If not ragdolling, then only allow the y coordinate to be updated
		if (!isRagdoll) {
			rb.velocity = new Vector3(0.0f, rb.velocity.y, 0.0f);
		}
	}

	private void FireProjectile() {
		if (Input.GetButtonDown("Fire1")) {
			// Calculate the tip of the cannon to spawn the bullet at
			Vector3 cannonExtents = cannon.GetComponent<Renderer>().bounds.extents;
			// The distance from center to tip is the forward vector scaled to the extents
			Vector3 distToTip = cannon.transform.forward;
			distToTip.Scale(cannonExtents);
			Vector3 cannonTip = cannon.transform.position + distToTip;

			GameObject projectile = Instantiate(standardBullet, cannonTip, Quaternion.identity);
			projectile.GetComponent<Projectile>().Init(cannon.transform.forward, cannon);
		}
	}

}
