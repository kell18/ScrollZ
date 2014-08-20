using UnityEngine;
using System.Collections;

public class WheelRotationScript : MonoBehaviour {
	public SpriteRenderer sprite;
		
	/// <summary>
	/// Update rotation of sprite same way this circle collider spins.
	/// </summary>
	void Update () {
		var spriteRotation = sprite.transform.rotation;
		spriteRotation = transform.rotation;
		sprite.transform.rotation = spriteRotation;
	}
}
