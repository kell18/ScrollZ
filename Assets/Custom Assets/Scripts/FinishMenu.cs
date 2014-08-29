using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]
public class FinishMenu : MonoBehaviour {
	public Texture ReplayImg;
	public bool IsWin;
	public string replayedLevel = "MainScene";
	public string SuccessMessageBody = "Your score: ";
	public string LooseMessageBody = "Sorry, you losts";
	// public Scorer Scorer;

	private GUIText Text;

	void Start() {
		Text = GetComponent<GUIText> ();
		Text.text = SuccessMessageBody; //+ Scorer.score;
	}

	void OnGUI() {
		if (GUI.Button(new Rect(25, 25, 100, 30), ReplayImg)) {
			Application.LoadLevel (replayedLevel); 
		}
	}
}
