using UnityEngine;
using System.Collections;

public class MotorBehaviourScript : MonoBehaviour {
	public WheelJoint2D DrivingWheel;	
	public float acceleration;
	public BoxCollider2D GasPedal;
	public BoxCollider2D BrakePedal;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		CheckKeyboardInput ();
	}

	private void CheckKeyboardInput() {
		float inputVect = Input.GetAxis ("Horizontal");
		JointMotor2D motor = DrivingWheel.motor;
		bool isUseMotor = true;
	
		if (GasIsPressed()) {
			motor.motorSpeed = motor.maxMotorTorque - motor.motorSpeed > 1 ? 
				motor.motorSpeed + acceleration
				: motor.maxMotorTorque;
		} else if (BrakeIsPressed()) {
			motor.motorSpeed = 0;
		} else {
			if (motor.motorSpeed > acceleration) {
				motor.motorSpeed -= Mathf.Pow(acceleration, 2);
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

		if (Input.GetMouseButton (0)) {
			if (GasPedal.OverlapPoint (clickPos)) {
				Debug.Log ("Pedal!");
				isPressed = true;
			}
		}
		if (Input.GetAxis ("Horizontal") > 0.01) {
			Debug.Log ("Key!");
			isPressed = true;
		}
		return isPressed;
	}

	private bool BrakeIsPressed() {
		bool isPressed = false;
		Vector3 mp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 clickPos = new Vector2 (mp.x, mp.y);

		if (Input.GetMouseButtonDown (0)) {
			if (BrakePedal.OverlapPoint (clickPos)) {
				isPressed = true;
			}
		}
		if (Input.GetAxis ("Horizontal") < -0.01) {
			isPressed = true;
		}
		return isPressed;
	}
}