using UnityEngine;
using System.Collections;

public class PointsForRotation : MonoBehaviour {
	public GroundedChecker GroundedChecker;
	public int TurnPoints = 100;
	public float AnglesTreshold = 300.0f;
	
	private float TraversedDegree = 0f;
	private float PrevDegree;
	
	void Start() {
		PrevDegree = transform.eulerAngles.z; 
	}
	
	void Update () {
		TraversedDegree += Mathf.Clamp (transform.eulerAngles.z - PrevDegree, -5, 5);
		if (TraversedDegree > AnglesTreshold || TraversedDegree < -AnglesTreshold) {
			Scorer.AddPoints (TurnPoints);
			TraversedDegree = 0;
		}
		if (GroundedChecker.IsGrounded) {
			TraversedDegree = 0;
		}
		PrevDegree = transform.eulerAngles.z; 
	}
}
