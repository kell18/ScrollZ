using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {
	public GameObject SpawningObject;
	public float MinSpawnFrequency = 10;
	public float MaxSpawnFrequency = 40;
	private float LastPos = 0;
	
	void FixedUpdate (){		
		if (transform.position.x - LastPos > Random.Range (MinSpawnFrequency, MaxSpawnFrequency)) {
			Vector2 newPosition = new Vector2 (transform.position.x, transform.position.y);
			SpawningObject.SetActive(true); // TODO: make pool
			var obj = Instantiate (SpawningObject, newPosition, Quaternion.identity);
			LastPos = transform.position.x;
		}
	}		
}