using UnityEngine;
using System.Collections;

public class WheelRotationBehaviour : MonoBehaviour {
	public SpriteRenderer sprite;
	public Vector3 prevPosition;

	void Start () {
		prevPosition = transform.localPosition;
	}
		
	/// <summary>
	/// Update rotation of sprite same way this circle collider spins.
	/// </summary>
	void FixedUpdate () {
		RotateSprite ();
	}

	void RotateSprite ()
	{
		var spriteRotation = sprite.transform.rotation;
		spriteRotation = transform.rotation;
		sprite.transform.rotation = spriteRotation;
	}
}
