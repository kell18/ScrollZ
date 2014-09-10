using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {
	public Transform Target;
	public float ScrollSpeed = 0.2f;
	
	// Update is called once per frame
	void FixedUpdate () {
		renderer.material.mainTextureOffset = new Vector2 (Target.position.x * ScrollSpeed, 0f);
	}
}
