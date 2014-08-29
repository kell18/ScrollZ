using UnityEngine;
using System.Collections;

public class AccelerationTracesEmitter : MonoBehaviour {
	public ParticleSystem Traces;
	public GroundedChecker GroundCheck;
	public int MaxEmissionRate = 500;
	public float EmissionFactor = 0.2f;
	public float AccelerationThreshold = 10f;

	private Vector2 acceleration { get; set; }
	private Vector2 lastVelocity { get; set; }

	void Start () {
		Traces.Stop ();
		lastVelocity = new Vector2 ();
	}

	void FixedUpdate () {
		if (GroundCheck.IsGrounded) {
			EmitTraces ();
		}
	}
	
	private void EmitTraces() {
		acceleration = (rigidbody2D.velocity - lastVelocity) / Time.fixedDeltaTime;
		lastVelocity = rigidbody2D.velocity;
		if (acceleration.x > AccelerationThreshold) {
			Traces.Emit((int)(acceleration.x * EmissionFactor));
		}
		else {
			Traces.Stop();
		}
	}
}
