using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GUIText))]
public class Scorer : MonoBehaviour {
	private static GUIText ScoreGUI;
	public static string ScoreText = "Score:";
	public static int ScoreVal = 0;

	void Start() {
		ScoreGUI = GetComponent<GUIText>();
		ScoreGUI.text = ScoreText + " <size=25><b>" + ScoreVal + "</b></size>";
	}

	public static void AddPoints(int value) {
		ScoreVal += value;
		ScoreGUI.text = ScoreText + " <size=25><b>" + ScoreVal + "</b></size>";
	}
	
	public static void DropPoints() {
		ScoreVal = 0;
		ScoreGUI.text = ScoreText + " <size=25><b>" + ScoreVal + "</b></size>";
	}
}
