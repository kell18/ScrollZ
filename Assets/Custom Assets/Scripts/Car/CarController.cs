using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {
	public WheelJoint2D DrivingWheel;
	public WheelJoint2D DrivenWheel;
	public BoxCollider2D GasPedal;
	public BoxCollider2D BrakePedal;
	public BoxCollider2D RotateUpPedal;
	public BoxCollider2D RotateDownPedal;
	public GroundedChecker AccelGroundCheck;
	public GroundedChecker RotationGroundCheck;
	public float DeltaAcceleration = 15;
	public float MaxAcceleration = 2000;
	public float MaxSpeed = 40;
	public float BoostPower = 100;
	public int AngularVelocityFactor = 200;
	public float ExtraAcceleration = 8;
	public Rigidbody2D ExtraAccelPoint;
	
	public float CurrentSpeed { get; private set; }
	public float RevsFactor { get; private set; }
	public float AccelInput { get; private set; }
	public float AvgSkid { get; private set; }
	public float SpeedFactor { get; private set; }
	private Vector2 Right;
	
	void Start() {
		AvgSkid = 0;
		Right = new Vector2 (transform.right.x,  transform.right.y);
	}
	
	// Update is called once per few frames
	void Update () {
		HandleAcceleration ();
		HandleRotation ();
		HandleBoost ();
		ComputeExtraProps ();
	}
	
	/// <summary>
	/// Handles the input from keyboard and mouse click over gas/brake pedals 
	/// then accelerate car.
	/// </summary>
	private void HandleAcceleration() {
		JointMotor2D driving = DrivingWheel.motor;
		bool isDrivingUseMotor = true;
		
		if (GasIsPressed()) {
			driving.motorSpeed = driving.motorSpeed - ComputeAcceleration(driving.motorSpeed);
			driving.motorSpeed = driving.motorSpeed < -MaxAcceleration ? -MaxAcceleration : driving.motorSpeed;
			if (AccelGroundCheck.IsGrounded && CurrentSpeed < MaxSpeed) {
				rigidbody2D.AddForce(Right * ExtraAcceleration/4f);
				ExtraAccelPoint.rigidbody2D.AddForce(Right * ExtraAcceleration/4f*3F);
			}
		} else if (BrakeIsPressed()) {
			driving.motorSpeed = 0;
		} else {
			if (driving.motorSpeed < -DeltaAcceleration) {
				driving.motorSpeed += DeltaAcceleration/2;
			} else {
				driving.motorSpeed = 0;
				isDrivingUseMotor = false;
			}
		}
		DrivingWheel.motor = driving;
		DrivingWheel.useMotor = isDrivingUseMotor;
	}
	
	private float ComputeAcceleration(float motorSpeed) {
		if (CurrentSpeed < 7 || motorSpeed > -700) {
			return DeltaAcceleration; // * (1.2f - SpeedFactor)
		}
		float actg = Mathf.Atan (-CurrentSpeed) + Mathf.PI/2;
		return actg * 30;
	}
	
	/// <summary>
	/// Handles the input from keyboard and mouse click over gas/brake pedals 
	/// then rotate car.
	/// </summary>
	private void HandleRotation() {
		if (!RotationGroundCheck.IsGrounded) {
			if (RotateUpIsPressed ()) {
				rigidbody2D.angularVelocity = +AngularVelocityFactor;
			} else if (RotateDownIsPressed ()) {
				rigidbody2D.angularVelocity = -AngularVelocityFactor;
			}
			else {
				rigidbody2D.angularVelocity = 0;
			}
		}
	}
	
	void HandleBoost ()
	{
		if (BoostIsPressed() && AccelGroundCheck.IsGrounded) {
			rigidbody2D.AddForce(transform.right * BoostPower);
		}
	}
	
	private void ComputeExtraProps() {
		RevsFactor = -(DrivingWheel.motor.motorSpeed / MaxAcceleration);
		CurrentSpeed = transform.InverseTransformDirection (rigidbody2D.velocity).x;
		SpeedFactor = Mathf.InverseLerp (0, MaxSpeed, Mathf.Abs (CurrentSpeed));
	}
	
	private bool GasIsPressed() {
		Vector3 mp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 clickPos = new Vector2 (mp.x, mp.y);
		float accelInput = Input.GetAxis ("Horizontal");
		AccelInput = accelInput;
		if (accelInput > 0.5) {
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
		if (Input.GetAxis ("Horizontal") < -0.5) {
			return true;
		} 
		else if (Input.GetMouseButton (0) && BrakePedal.OverlapPoint (clickPos)) {
			return true;
		}
		return false;
	}
	
	private bool BoostIsPressed() {
		return Input.GetKey (KeyCode.Space);
	}
	
	private bool RotateUpIsPressed() {
		Vector3 mp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 clickPos = new Vector2 (mp.x, mp.y);
		if (Input.GetAxis ("Vertical") > 0.5) {
			return true;
		} 
		else if (Input.GetMouseButton (0) && RotateUpPedal.OverlapPoint (clickPos)) {
			return true;
		}
		return false;
	}
	
	private bool RotateDownIsPressed() {
		Vector3 mp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 clickPos = new Vector2 (mp.x, mp.y);
		if (Input.GetAxis ("Vertical") < -0.5) {
			return true;
		} 
		else if (Input.GetMouseButton (0) && RotateDownPedal.OverlapPoint (clickPos)) {
			return true;
		}
		return false;
	}
}