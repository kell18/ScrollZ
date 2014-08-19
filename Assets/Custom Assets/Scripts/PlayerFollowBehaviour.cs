using UnityEngine;
using System.Collections;

public class PlayerFollowBehaviour : MonoBehaviour {
	public Transform Target;
	
	// Update is called once per frame
	void Update () {
		var selPos = transform.position;
		selPos.x = Target.position.x;
		selPos.y = Target.position.y;
		transform.position = selPos;
	}
}
