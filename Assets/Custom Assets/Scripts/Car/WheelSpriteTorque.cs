using UnityEngine;
using System.Collections;

public class WheelSpriteTorque : MonoBehaviour {
	public SpriteRenderer sprite;
	
	void FixedUpdate () {
		var spriteRot = sprite.transform.rotation;
		var spritePos = sprite.transform.position;
		spriteRot = transform.rotation;
		spritePos = transform.position;
		sprite.transform.rotation = spriteRot;
		sprite.transform.position = spritePos;
	}
}
