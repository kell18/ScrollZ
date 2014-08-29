using UnityEngine;
using System.Collections;

public class CarRotationScore : MonoBehaviour
{
		private float Flag1;
		private float Flag2;
		private bool IsCanTakePoints;		

		// Update is called once per frame
	
		void Update ()
		{
				CRotation ();
		}

		void CRotation ()
		{
				if (Mathf.Abs (Mathf.Abs (transform.eulerAngles.z) - 180) < 0.5 && IsCanTakePoints) {	
						Flag1 = rigidbody2D.angularVelocity;
						if (Flag1 > 0) {
								GetComponent<Scorer> ().Score += 20;
						} else
								GetComponent<Scorer> ().Score += 25;
						IsCanTakePoints = false;
				}
				if (Mathf.Abs (transform.eulerAngles.z) < 0.5) {
						IsCanTakePoints = true;
				}
			
		}
}
