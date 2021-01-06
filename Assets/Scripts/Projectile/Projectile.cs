using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	// Children references
	private Rigidbody rb;

	// Member variables
	private Vector3 oldVelocity;    // Stores the velocity of the projectile on the previous frame
	private GameObject source;      // The object that produced this projectile

	private float moveSpeed = 20.0f;

	public void Init(Vector3 direction, GameObject source) {
		rb.velocity = direction.normalized * moveSpeed;
		this.source = source;
		Physics.IgnoreCollision(source.gameObject.GetComponent<Collider>(), this.gameObject.GetComponent<Collider>(), true);
	}

	private void Awake() {
		rb = this.GetComponent<Rigidbody>();

		// Freeze the rotation so projectile doesn't go spinning after a collision
		rb.freezeRotation = true;
	}

	// Start is called before the first frame update
	void Start() {

	}

    // Update is called once per frame
    void Update() {
		
    }

	private void FixedUpdate() {
		// Store the old velocity for later use
		oldVelocity = rb.velocity;
	}

	private void OnCollisionEnter(Collision collision) {
		// Get the point of contact
		ContactPoint contact = collision.contacts[0];
		// Reflect our old velocity off the contact point's normal vector
		Vector3 reflectedVector = Vector3.Reflect(oldVelocity, contact.normal);
		rb.velocity = reflectedVector;

		// Rotate the object by the same amount we changed its velocity
		Quaternion reflectedRotation = Quaternion.FromToRotation(oldVelocity, reflectedVector);
		this.transform.rotation *= reflectedRotation;
	}

	private void OnCollisionExit(Collision collision) {
		if (collision.gameObject == source) {
			Physics.IgnoreCollision(source.gameObject.GetComponent<Collider>(), this.gameObject.GetComponent<Collider>(), false);
		}
	}

}
