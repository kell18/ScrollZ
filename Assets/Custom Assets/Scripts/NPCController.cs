using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
public class NPCController : DismemberedObject {
	public GameObject Player;
	public AudioClip DismemberSound1;
	public AudioClip DismemberSound2;
	public AudioClip DismemberSound3;
	public AudioClip DismemberSound4;
	public AudioClip DismemberSound5;
	public AudioClip DismemberSound6;
	public float MoveSpeed = 15;
	public float AttackDistance = 3;
	public string AnimatorMoveSpeedParam = "MoveSpeed";
	public string AnimatorStrikeForceParam = "StrikeForce";
	public string AnimatorRandomDeathParam = "IsRandomDeath";
	
	protected Animator Animator;
	protected const int NotCollidePlayerLayer = 9;

	private float _direction = 1;
	private bool _isDead = false;
	private AudioSource[] _audioSources;
	
	void Start() {
		_audioSources = new AudioSource[6];
		_audioSources[0] = SetUpAudioSource (DismemberSound1);
		_audioSources[1] = SetUpAudioSource (DismemberSound2);
		_audioSources[2] = SetUpAudioSource (DismemberSound3);
		_audioSources[3] = SetUpAudioSource (DismemberSound4);
		_audioSources[4] = SetUpAudioSource (DismemberSound5);
		_audioSources[5] = SetUpAudioSource (DismemberSound6);
		Animator = GetComponent<Animator>();
	}

	/// <summary>
	/// Move to player and then attack.
	/// </summary>
	void FixedUpdate () {
		if (GoTo (Player.transform.position.x)) {
			TryToAttack(Player);
		}

	}

	/// <summary>
	/// Calls when animator exit from death animation.
	/// </summary>
	void DeathAnimationExitEvent () {
		gameObject.SetActive (false);
	}

	/// <summary>
	/// Fabric method forom dismemberedObject.
	/// Destroy NPC before applying force.
	/// </summary>
	/// <returns>Is continue execute ApplyForce?</returns>
	/// <param name="strikeForce">Strike force.</param>
	/// <param name="collision">Collision object</param>
	protected override bool BeforeApplyForce(float strikeForce, Vector2 collisionNormal) {
		if (!_isDead) {
			rigidbody2D.isKinematic = true;
			Destroy(collider2D);
			_isDead = true;
			SetSpeed (0);
			Animator.SetFloat (AnimatorStrikeForceParam, strikeForce);
			Animator.SetBool (AnimatorRandomDeathParam, Random.value > 0.5f);
			_audioSources [Random.Range(0, 6)].Play ();
			gameObject.layer = NotCollidePlayerLayer;
			return true;
		} else {
			return false;
		}
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

	private AudioSource SetUpAudioSource(AudioClip clip) {
		AudioSource source = gameObject.AddComponent<AudioSource>();
		source.playOnAwake = false;
		source.volume = DismemberClipVolume;
		source.clip = clip;
		return source;
	}
}
