using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour {
	
	public AudioClip deathSound;
	public AudioClip screamSound;

	private Animator mAnimator;
	private PlayerRunner mRunner;

	void Start() {
		mAnimator = GetComponent<Animator> ();
		mRunner = GetComponent<PlayerRunner> ();
		Game.State.OnStateChanged += StateChanged;
	}

	void Update() {
		mAnimator.SetBool("OnAir", mRunner.isOnAir);
	}
	
	void StateChanged (GameStateEnum oldState, GameStateEnum newState) {
		if (newState == GameStateEnum.GameOver) {
			mAnimator.SetTrigger("Death");
			AudioSource.PlayClipAtPoint(deathSound, Vector3.zero);
			AudioSource.PlayClipAtPoint(screamSound, Vector3.zero);
		}
	}
}
