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
		JointMotor2D driving = DrivingWheel.motor;
		JointMotor2D driven = DrivenWheel.motor;
		bool isDrivingUseMotor = true;
		bool isDrivenUseMotor = true;
	
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
			isDrivenUseMotor = false;
		}
		DrivingWheel.motor = driving;
		DrivingWheel.useMotor = isDrivingUseMotor;
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
		else if (Input.GetMouseButton (0) && BrakePedal.OverlapPoint (clickPos)) {
			isPressed = true;
		}
		return isPressed;
	}
}