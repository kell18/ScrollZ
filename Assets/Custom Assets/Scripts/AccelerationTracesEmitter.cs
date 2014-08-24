using UnityEngine;
using System.Collections;

public class AccelerationTracesEmitter : MonoBehaviour {
	public ParticleSystem Traces;
	public GroundedChecker GroundCheck;
	public int MaxEmissionRate = 500;
	public float AccelerationThreshold = 10;

	private Vector2 acceleration { get; set; }
	private Vector2 lastVelocity { get; set; }

	void Start () {
		Traces.Pause ();
		lastVelocity = new Vector2 ();
	}

	void FixedUpdate () {
		TryToEmitTraces ();
	}
	
	private void TryToEmitTraces() {
		bool isGrounded = GroundCheck.IsGrounded;
		acceleration = (rigidbody2D.velocity - lastVelocity) / Time.fixedDeltaTime;
		lastVelocity = rigidbody2D.velocity;
		if (acceleration.x > AccelerationThreshold && isGrounded) {
			Traces.Emit((int)acceleration.x);
		}
		else {
			Traces.Pause ();
		}
	}
}
