using UnityEngine;
using System.Collections;

public class AnimatorsPool : MonoBehaviour {
	public Animator Instance;
	public float PoolSize = 100;
	public string Type { get; private set;}
	
	private Animator[] Pool;
	private int NextRealized = 0;
	
	public Animator GetAnimator() {
		if (NextRealized < PoolSize) {
			NextRealized += 1;
			var animator = Pool[NextRealized];
			Pool[NextRealized] = null;
			return animator;
		}
		return null;
	}
	
	public void PutAnimator(Animator animator) {
		if (NextRealized > 0) {
			NextRealized -= 1;
			Pool[NextRealized] = animator;
		}
	}
	
	// Use this for initialization
	void Start () {
		for (int i = 0; i < PoolSize; i++) {
			Pool[i] = Instantiate(Instance) as Animator;
		}
		Type = Pool [0].name;
	}
}
