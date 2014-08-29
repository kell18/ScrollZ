using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class ShootingController : MonoBehaviour {
	public Transform StartPoint;
	public Animator ShotAnimator;
	public Animator ExplosionAnimator;
	public string ShotAnimationName = "Shot";
	public string ExplosionAnimationName = "Explosion1";
	public AudioClip ShotAudioClip;
	public float ShotPower = 100;
	public float ShotDistance = 5000;
	public string EnemyTagName = "Enemy";

	private AudioSource AudioSource;

	public void Start() {
		AudioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) || Input.GetKeyDown(KeyCode.LeftControl)) {
			Shoot();
		}
	}

	private void Shoot() {
		SpawnShot (StartPoint.position);
		RaycastHit2D hit = Physics2D.Raycast(StartPoint.position, transform.right, ShotDistance);
		if (hit.collider == null) {
			return;
		}
		if (hit.collider.gameObject.CompareTag (EnemyTagName)) {
			var npc = hit.collider.gameObject.GetComponent<NPCController>();
			npc.ApplyForce (ShotPower, -hit.normal);
		}
		SpawnExplosion(hit.point);
	}

	private void SpawnShot (Vector3 startPoint) {
		Instantiate (ShotAnimator, startPoint, Quaternion.identity); // TODO: Change to object pool
		AudioSource.PlayOneShot (ShotAudioClip, 1);
		ShotAnimator.gameObject.SetActive (true);
		ShotAnimator.Play (ShotAnimationName);
	}

	private void SpawnExplosion (Vector2 hitPoint) {
		Instantiate (ExplosionAnimator, hitPoint, Quaternion.identity); // TODO: Change to object pool
		ExplosionAnimator.gameObject.SetActive (true);
		ExplosionAnimator.Play (ExplosionAnimationName);
	}
}
