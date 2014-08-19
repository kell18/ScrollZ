using UnityEngine;
using System.Collections;

public class MotorBehaviourScript : MonoBehaviour {
	public WheelJoint2D DrivingWheel;	
	public float acceleration = 100;
	public BoxCollider2D GasPedal;
	public BoxCollider2D BrakePedal;
	public Rigidbody2D Car = new Rigidbody2D();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		CheckKeyboardInput ();
		CheckMouseInput ();
	}

	private void CheckKeyboardInput() {
		float inputVect = Input.GetAxis ("Horizontal");
		JointMotor2D motor = DrivingWheel.motor;
		bool isUseMotor = true;
		if (inputVect == 1) {
			// TODO: Refactor
			motor.motorSpeed = motor.maxMotorTorque - motor.motorSpeed > 3 ? 
				motor.motorSpeed + Mathf.Sqrt(motor.motorSpeed + acceleration)
				: motor.motorSpeed;
		} else if (inputVect == -1) {
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

	private void CheckMouseInput() {
		// TODO: write it
		float mouseX = Input.mousePosition.x;
		float mouseY = Input.mousePosition.y;
	}
}
