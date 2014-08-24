using UnityEngine;
using System.Collections;

public class NPCController : DismemberedObject {
	public GameObject Player;
	public float MoveSpeed = 15;
	public float AttackDistance = 3;
	public string AnimatorMoveSpeedParam = "MoveSpeed";

	private float _direction = 1;
	private bool _isDead = false;

	/// <summary>
	/// Move to player and attack then.
	/// </summary>
	void FixedUpdate () {
		if (GoTo (Player.transform.position.x)) {
			TryToAttack(Player);
		}
	}

	/// <summary>
	/// Calls when animator exit from death animation.
	/// </summary>
	void DeathAnimationExitEvent(){
		gameObject.SetActive (false);
	}

	/// <summary>
	/// Call parentmethod and then deativate collided NPC.
	/// </summary>
	public new void OnCollisionEnter2D(Collision2D collision) {
		base.OnCollisionEnter2D(collision);
		if (collision.gameObject.isStatic) {
			return;
		}
		gameObject.layer = NotCollidePlayerLayer;
		_isDead = true;
		SetSpeed (0);
	}

	protected bool GoTo(float xCoord) {
		if (_isDead) {
			return false;
		}
		if (Mathf.Abs(xCoord - transform.position.x) <= AttackDistance) {
			return true;
		}
		TurnTo (xCoord);
		SetSpeed (Mathf.Abs (MoveSpeed) * _direction);
		return false;
	}

	protected void TurnTo(float xCoord) {
		var scale = transform.localScale;
		if (xCoord > transform.position.x) {
			scale.x *= scale.x > 0 ? 1 : -1;
			_direction = 1;
		} else {
			scale.x *= scale.x < 0 ? 1 : -1;
			_direction = -1;
		}
		transform.localScale = scale;
	}

	protected void SetSpeed(float value) {
		var velocity = rigidbody2D.velocity;
		velocity.x = value;
		Animator.SetFloat (AnimatorMoveSpeedParam, Mathf.Abs (velocity.x));
		rigidbody2D.velocity = velocity;
	}

	protected bool TryToAttack(GameObject obj) {
		SetSpeed(0);
		return true;
	}
}
