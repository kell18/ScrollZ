using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class FinishingCheck : MonoBehaviour {
	public GameObject FinishMenu;
	public CarAudio CarAudio;
	public string PlayerTag = "Player";

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag(PlayerTag)) {
			FinishMenu.SetActive(true);
			Time.timeScale = 0f;
			CarAudio.pitchMultiplier = 0.01f;
		}
		// Mathf.Lerp(Time.timeScale, 0f, Time.deltaTime * Smooth);
	}
}
