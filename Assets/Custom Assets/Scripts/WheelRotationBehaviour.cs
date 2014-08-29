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
		//Debug.Log (gameObject.name + transform.position);
		RotateAndMoveSprite ();
	}

	void RotateAndMoveSprite ()
	{
		var spriteRot = sprite.transform.rotation;
		var spritePos = sprite.transform.position;
		spriteRot = transform.rotation;
		spritePos = transform.position;
		sprite.transform.rotation = spriteRot;
		sprite.transform.position = spritePos;
	}
}
