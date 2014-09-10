using UnityEngine;
using System.Collections.Specialized;
using System.Collections.Generic;

public class GameObjectPool {
	private Queue<GameObject> Pool;
	private GameObject PoolUnit;
	private Transform UnitParent;
	private int PoolSize;
	private ResetInstance ResetInstance;
	
	public bool Return(GameObject poolUnit) {
		bool IsFull = Pool.Count < PoolSize;
		if (!IsFull) {
			Pool.Enqueue(ResetInstance(poolUnit));
		}
		return IsFull;
	}
	
	public GameObject Take() {
		if (Pool.Count > 0) { 
			return Pool.Dequeue();
		}
		return null;
	}
	 
	private void FloodPool() {
		GameObject unit;
		while (PoolSize-- > 0) {
			unit = ResetInstance(Object.Instantiate(PoolUnit) as GameObject);
			unit.transform.parent = UnitParent;
			Pool.Enqueue(unit);
		}
	}
	
	public GameObjectPool (GameObject poolUnit, Transform unitParent, 
		int poolSize, ResetInstance beforeReturn) {
		
		this.PoolUnit = poolUnit;
		this.UnitParent = unitParent;
		this.PoolSize = poolSize;
		this.ResetInstance = beforeReturn;
		FloodPool();
	}
	
}

public delegate GameObject ResetInstance(GameObject poolUnit);
