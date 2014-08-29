using UnityEngine;
using System.Collections;

public class GroundedChecker : MonoBehaviour {
	public LayerMask GroundLayer;
	public GameObject CollidedObject;
	public bool IsGrounded { get; private set; }

	/// <summary>
	/// Determs radius of collision detection. 
	/// -1 means that radius determs form CollidedObject.collider2D
	/// </summary>
	public float CheckRadius = -1;



	// Update is called once per frame
	void FixedUpdate () {
		if (CheckRadius == -1) {
			if (CollidedObject.collider2D == null) {
				Debug.LogError ("CheckRadius cannot be equals -1 when CollidedObject has`t collider2D." +
				                "Add colleder2D to yours '"+CollidedObject.name + "' object");
			}
			CheckRadius = CollidedObject.collider2D.bounds.size.y/2;
		}
		IsGrounded = Physics2D.OverlapCircle (
			CollidedObject.gameObject.transform.position, CheckRadius, GroundLayer
		);
	}
}
