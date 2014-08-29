using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Dismember object by cild objects of current.
/// </summary>
public class DismemberedObject : MonoBehaviour {
	public float DismemberClipVolume = 0.5f;
	public float DismemberStrikeForce = 15f;
	public float DispersionMultiplier = 50f;

	/// <summary>
	/// Handles collisions with non staticl game objects,
	/// and dismember this object 
	/// if strike force > DismemberStrikeForce.
	/// </summary>
	public void OnCollisionEnter2D(Collision2D collision) {
		if (!collision.gameObject.isStatic) {
			float strikeForce = ComputeStrikeForce(collision);
			ApplyForce(strikeForce, collision.contacts [0].normal);
		}
	}

	/// <summary>
	/// Applies the force to object, set param to animator and 
	/// dismember object if strikeForce > DismemberStrikeForce.
	/// </summary>
	/// <param name="strikeForce">Strike force.</param>
	/// <param name="collisionNormal">Direction of dismember</param>
	public void ApplyForce(float strikeForce, Vector2 collisionNormal) {
		if (!BeforeApplyForce(strikeForce, collisionNormal)) {
			return;
		}

		if (strikeForce >= DismemberStrikeForce) {
			Transform dTransform = transform;
			Transform child;
			Vector2 direction = collisionNormal;
			for (int i = 0; i < dTransform.childCount; i++) {
				child = dTransform.GetChild (i);
				if (child.rigidbody2D != null) {
					direction.x += Random.Range (-0.1F, 0.3F);
					direction.y += Random.Range (-0.1F, 0.3F);
					child.gameObject.SetActive(true);
					child.rigidbody2D.angularVelocity += Random.value * 1000f;
					child.rigidbody2D.AddForce (direction * (strikeForce * Random.value*5) * DispersionMultiplier);
				}
			}
		}
	}

	/// <summary>
	/// Fabric method for extra logic in subclasses.
	/// Call before method ApplyForce.
	/// </summary>
	/// <returns>Is continue perfoem ApplyForce</returns>
	/// <param name="strikeForce">Strike force.</param>
	/// <param name="collision">Collision object</param>
	protected virtual bool BeforeApplyForce(float strikeForce, Vector2 collisionNormal) {
		return true;
	}

	private float ComputeStrikeForce (Collision2D collision)
	{
		return 0.5f * collision.rigidbody.mass * collision.relativeVelocity.magnitude;
	}
}
