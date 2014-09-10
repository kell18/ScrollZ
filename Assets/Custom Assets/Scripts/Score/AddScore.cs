using UnityEngine;
using System.Collections;

public class AddScore : MonoBehaviour
{
	public GameObject Car;
	public Texture2D Tex;
	public bool b = false;
	
	void Start ()
	{ 
			Car = GameObject.Find ("Car");			
	}

	void OnCollisionEnter2D (Collision2D coll)
	{ 
		if (coll.gameObject == Car) { 
			Scorer.AddPoints(10); 							
			b = true;			
		}				
	}	
}
