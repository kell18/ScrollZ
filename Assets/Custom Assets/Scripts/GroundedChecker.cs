using UnityEngine;
using System.Collections;

public class GroundedChecker : MonoBehaviour {
	public LayerMask GroundLayer;
	public GameObject GroundCheck;
	public float CheckRadius = 0.02f;
	public bool IsGrounded { get; private set; }

	// Update is called once per frame
	void FixedUpdate () {
		IsGrounded = Physics2D.OverlapCircle (
			GroundCheck.gameObject.transform.position, CheckRadius, GroundLayer
		);
	}
}
