using UnityEngine;
using System.Collections;

public class ObjectFollower : MonoBehaviour {
	public Transform Target;
	public float MinYPos = -17.5f;
	
	// Update position this object same Target object.
	void FixedUpdate () {
		var selfPos = transform.position;
		selfPos.x = Target.position.x;
		if (Target.position.y > MinYPos) {
			selfPos.y = Target.position.y;
		} 
		transform.position = selfPos;
	}
}
