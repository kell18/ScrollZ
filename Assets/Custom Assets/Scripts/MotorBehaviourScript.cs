using UnityEngine;
using System.Collections;

public class MotorBehaviourScript : MonoBehaviour {
	public float Acceleration;
	public float MaxSpeed;
	public WheelJoint2D DrivingWheel;
	public WheelJoint2D DrivenWheel;
	public BoxCollider2D GasPedal;
	public BoxCollider2D BrakePedal;
	
	// Update is called once per few frames
	void FixedUpdate () {
		HandleInput ();
	}

	/// <summary>
	/// Handles the input from keyboard and mouse click over gas/brake pedals.
	/// </summary>
	private void HandleInput() {
		float inputVect = Input.GetAxis ("Horizontal");
		JointMotor2D motor = DrivingWheel.motor;
		bool isUseMotor = true;
	
		if (GasIsPressed()) {
			motor.motorSpeed = MaxSpeed - motor.motorSpeed > 1 ? 
				(motor.motorSpeed + Acceleration) : MaxSpeed;
		} else if (BrakeIsPressed()) {
			motor.motorSpeed = 0;
		} else {
			if (motor.motorSpeed > Acceleration) {
				motor.motorSpeed -= Acceleration;
			} else {
				motor.motorSpeed = 0;
				isUseMotor = false;
			}
		}
		DrivingWheel.motor = motor;
		DrivingWheel.useMotor = isUseMotor;
	}

	private bool GasIsPressed() {
		bool isPressed = false;
		Vector3 mp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 clickPos = new Vector2 (mp.x, mp.y);
		if (Input.GetAxis ("Horizontal") > 0.01) {
			isPressed = true;
		}
		else if (Input.GetMouseButton (0) && GasPedal.OverlapPoint (clickPos)) {
			isPressed = true;
		}
		return isPressed;
	}

	private bool BrakeIsPressed() {
		bool isPressed = false;
		Vector3 mp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 clickPos = new Vector2 (mp.x, mp.y);
		if (Input.GetAxis ("Horizontal") < -0.01) {
			isPressed = true;
		} 
		else if (Input.GetMouseButtonDown (0) && BrakePedal.OverlapPoint (clickPos)) {
			isPressed = true;
		}
		return isPressed;
	}
}