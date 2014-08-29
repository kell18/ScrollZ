using UnityEngine;
using System.Collections;

public class Scorer : MonoBehaviour {
	public int Score;
	//public GUIText Text;

	void OnGUI ()
	{ 
		//Text.text = "Score: " + Score;
		GUI.contentColor = Color.green;
		GUI.Label (new Rect (300, 10, 10, 10), " Score" + Score);
	} 

			
}
