using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour {
	private Animator mAnimator;
	private PlayerRunner mRunner;

	void Start() {
		mAnimator = GetComponent<Animator> ();
		mRunner = GetComponent<PlayerRunner> ();
	}

	void Update() {
		mAnimator.SetBool("OnAir", mRunner.isOnAir);
	}
}
