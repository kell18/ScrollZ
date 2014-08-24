using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {
	public WheelJoint2D DrivingWheel;
	public WheelJoint2D DrivenWheel;
	public BoxCollider2D GasPedal;
	public BoxCollider2D BrakePedal;
	public AudioSource AudioSrc;
	public GroundedChecker GroundCheck;
	public float Acceleration = 25;
	public float MaxSpeed = 2000;
	public int AngularVelocityFactor = -300;

	public float CurrentSpeed { get; set; }
	
	// Update is called once per few frames
	void FixedUpdate () {
		if (GroundCheck.IsGrounded) {
			HandleAcceleration ();
		} else {
			HandleRotation ();
		}
	}

	/// <summary>
	/// Handles the input from keyboard and mouse click over gas/brake pedals.
	/// </summary>
	private void HandleAcceleration() {
		JointMotor2D driving = DrivingWheel.motor;
		JointMotor2D driven = DrivenWheel.motor;
		bool isDrivingUseMotor = true;
	
		if (GasIsPressed()) {
			driving.motorSpeed = MaxSpeed - driving.motorSpeed > 1 ? 
				(driving.motorSpeed + Acceleration) : MaxSpeed;
		} else if (BrakeIsPressed()) {
			driving.motorSpeed = 0;
			driven.motorSpeed = 0;
		} else {
			if (driving.motorSpeed > Acceleration) {
				driving.motorSpeed -= Acceleration;
			} else {
				driving.motorSpeed = 0;
				isDrivingUseMotor = false;
			}
		}
		CurrentSpeed = driving.motorSpeed;
		DrivingWheel.motor = driving;
		DrivingWheel.useMotor = isDrivingUseMotor;
	}

	private void HandleRotation() {
		if (GasIsPressed()) {
			rigidbody2D.angularVelocity = AngularVelocityFactor;
		} else if (BrakeIsPressed()) {
			rigidbody2D.angularVelocity = -AngularVelocityFactor;
		}
	}

	private void EmitSound() {
		if (AudioSrc != null) {
			AudioSrc.pitch = 1 / (MaxSpeed / CurrentSpeed + 0.01f) * 3;
		}
	}

	private bool GasIsPressed() {
		Vector3 mp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 clickPos = new Vector2 (mp.x, mp.y);
		if (Input.GetAxis ("Horizontal") > 0.01) {
			return true;
		}
		else if (Input.GetMouseButton (0) && GasPedal.OverlapPoint (clickPos)) {
			return true;
		}
		return false;
	}

	private bool BrakeIsPressed() {
		bool isPressed = false;
		Vector3 mp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 clickPos = new Vector2 (mp.x, mp.y);
		if (Input.GetAxis ("Horizontal") < -0.01) {
			isPressed = true;
		} 
		else if (Input.GetMouseButton (0) && BrakePedal.OverlapPoint (clickPos)) {
			isPressed = true;
		}
		return isPressed;
	}
}