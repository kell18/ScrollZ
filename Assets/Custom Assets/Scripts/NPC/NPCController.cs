using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
public class NPCController : DismemberedObject {
	public AudioClip[] DeathClips;
	[Range(0,1)] public float DeathClipsVolume = 0.5f;
	public GameObject Player;
	public float MoveSpeed = 15;
	public float AttackDistance = 1;
	public int ScorePoints = 10;
	
	protected Animator Animator;
	protected const int NotCollidePlayerLayer = 9;
	protected string AnimatorMoveSpeedParam = "MoveSpeed";
	protected string AnimatorStrikeForceParam = "StrikeForce";
	protected string AnimatorRandomDeathParam = "IsRandomDeath";
	
	private float _direction = 1;
	private bool _isDead = false;
	private AudioSource[] _audioSources;
	
	void Start() {
		_audioSources = new AudioSource[DeathClips.Length];
		for (int i = 0; i < DeathClips.Length; i++) {
			_audioSources[i] = SetUpAudioSource (DeathClips[i]);
		}
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
	/// Destroy NPC before dismembering.
	/// </summary>
	/// <returns>Is continue execute ApplyForce?</returns>
	/// <param name="strikeForce">Strike force.</param>
	/// <param name="collision">Collision object</param>
	protected override bool BeforeDismember(float strikeForce, Vector2 collisionNormal) {
		if (!_isDead) {
			rigidbody2D.isKinematic = true;
			collider2D.enabled = false;
			_isDead = true;
			SetSpeed (0);
			Animator.SetFloat (AnimatorStrikeForceParam, strikeForce);
			Animator.SetBool (AnimatorRandomDeathParam, Random.value > 0.6f);
			_audioSources [Random.Range (0, _audioSources.Length)].Play ();
			gameObject.layer = NotCollidePlayerLayer;
			Scorer.AddPoints (ScorePoints);
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
		source.volume = DeathClipsVolume;
		source.clip = clip;
		return source;
	}
}
