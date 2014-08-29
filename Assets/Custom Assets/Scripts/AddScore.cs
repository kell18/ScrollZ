using UnityEngine;
using System.Collections;

public class AddScore : MonoBehaviour
{
		public GameObject Car;
		public Texture2D Tex;
		public bool b = false;
	    
		
		// Update is called once per frame
		void Update ()
		{
	
		}

		void Start ()
		{ 
				Car = GameObject.Find ("Car");			
		}

		void OnCollisionEnter2D (Collision2D coll)
		{ 
				if (coll.gameObject == Car) { 
						Car.GetComponent<Scorer> ().Score += 10; 							
						b = true;			
				}				
		}

		/*void OnGUI ()
		{ 
				
				if (b)		
		
						GUI.Label (new Rect (300, 10, 100, 100), Tex);
		
		} */		
}
