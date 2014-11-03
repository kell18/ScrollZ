using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Dismembers the object on hit by the Limbs array
/// </summary>
public class DismemberedObject : MonoBehaviour {
	public Transform[] Limbs;
	public float DismemberForceThreshold = 15f;
	public float DismemberForceMultiplier = 40f;

	protected const float AngularVelMultiplier = 1000f;

	/// <summary>
	/// Handles collisions with non staticl game objects,
	/// and dismember this object 
	/// if strike force > DismemberStrikeForce.
	/// </summary>
	public void OnCollisionEnter2D(Collision2D collision) {
		if (!collision.gameObject.isStatic) {
			float strikeForce = ComputeStrikeForce(collision);
			Dismember(strikeForce, collision.contacts [0].normal);
		}
	}

	/// <summary>
	/// Applies the force to object, set param to animator and 
	/// dismember object if strikeForce > DismemberStrikeForce.
	/// </summary>
	/// <param name="strikeForce">Strike force.</param>
	/// <param name="collisionNormal">Direction of dismember</param>
	public void Dismember(float strikeForce, Vector2 collisionNormal) {
		if (!BeforeDismember(strikeForce, collisionNormal)) {
			return;
		}
		if (strikeForce >= DismemberForceThreshold) {
			Transform child;
			Vector2 direction = collisionNormal;
			for (int i = 0; i < Limbs.Length; i++) {
				child = Limbs[i];
				if (child.rigidbody2D != null) {
					direction.x += Random.Range (-0.1F, 0.3F);
					direction.y += Random.Range (-0.1F, 0.3F);
					child.gameObject.SetActive(true);
					child.rigidbody2D.angularVelocity += Random.value * AngularVelMultiplier;
					child.rigidbody2D.AddForce (direction * (strikeForce * Random.value*5) * 
									DismemberForceMultiplier);
				}
			}
		}
	}

	/// <summary>
	/// Fabric method for extra logic in subclasses.
	/// Call before Dismember method.
	/// </summary>
	/// <returns>Is continue perfoem ApplyForce</returns>
	/// <param name="strikeForce">Strike force.</param>
	/// <param name="collision">Collision object</param>
	protected virtual bool BeforeDismember(float strikeForce, Vector2 collisionNormal) {
		return true;
	}

	private float ComputeStrikeForce (Collision2D collision)
	{
		return 0.5f * collision.rigidbody.mass * collision.relativeVelocity.magnitude;
	}
}
