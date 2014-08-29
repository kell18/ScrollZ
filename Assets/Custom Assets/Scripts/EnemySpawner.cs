using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject Enemy;
	public float MinSpawnFrequency;
	public float MaxSpawnFrequency;
	private float LastPos = 0;
	// Update position this object same Target object.
	void FixedUpdate (){		
		if (transform.position.x - LastPos > Random.Range (MinSpawnFrequency, MaxSpawnFrequency)) {
			Vector2 newPosition = new Vector2 (transform.position.x, transform.position.y);
			Enemy.SetActive(true);
			var enemy = Instantiate (Enemy, newPosition, Quaternion.identity);
			LastPos = transform.position.x;
		}
	}		
}