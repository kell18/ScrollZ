using UnityEngine;
using System.Collections;

public class DismemberedObject : MonoBehaviour {
	public GameObject DismemberedParts;
	public float DismemberStrikeForce = 15;
	public float DispersionMultiplier = 50;
	public string AnimatorStrikeParam = "StrikeForce";

	protected Animator Animator;
	protected const int NotCollidePlayerLayer = 9;

	void Start() {
		Animator = GetComponent<Animator>();
	}

	/// <summary>
	/// Handles collisions with non staticl game objects,
	/// set float "StrikeForce" to animator and dismember this object
	/// if strike force > DismemberStrikeForce.
	/// </summary>
	public void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.isStatic) {
			return;
		}
		float strikeForce = ComputeStrikeForce(collision);
		Animator.SetFloat (AnimatorStrikeParam, strikeForce);
		if (strikeForce > DismemberStrikeForce) {
			DismemberObject(strikeForce, collision.contacts[0].normal);
			gameObject.layer = NotCollidePlayerLayer;
		}
	}

	private void DismemberObject(float strikeForce, Vector2 collisionNormal) {
		Transform dTransform = DismemberedParts.transform;
		Transform child;
		Vector2 direction = collisionNormal;
		for (int i = 0; i < dTransform.childCount; i++) {
			direction.x += Random.Range(-0.1F, 0.3F);
			direction.y += Random.Range(-0.1F, 0.3F);
			child = dTransform.GetChild(i);
			child.position = Animator.transform.position;
			child.rigidbody2D.isKinematic = false;
			child.rigidbody2D.AddForce(direction * strikeForce * DispersionMultiplier);
		}
	}

	private float ComputeStrikeForce (Collision2D collision)
	{
		return 0.5f * collision.rigidbody.mass * collision.relativeVelocity.magnitude;
	}
}
