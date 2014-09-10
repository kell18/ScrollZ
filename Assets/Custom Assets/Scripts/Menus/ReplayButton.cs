using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]
public class ReplayButton : MonoBehaviour {
	public string replayedLevel = "MainScene";
	private GUIText ReplayText;

	// Use this for initialization
	void Start () {
		ReplayText = GetComponent<GUIText> ();
	}
	
	void OnMouseEnter() {
		ReplayText.color = Color.red;
	}
	
	void OnMouseExit() {
		ReplayText.color = Color.white;
	}
	
	void OnMouseUp() {
		Application.LoadLevel (replayedLevel);
		Time.timeScale = 1f;
		Scorer.DropPoints();
	}
}
