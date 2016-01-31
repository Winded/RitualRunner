using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour {
	private Animator mAnimator;
	private AudioSource mAudio;
	private PlayerRunner mRunner;

	void Start() {
		mAnimator = GetComponent<Animator> ();
		mAudio = GetComponent<AudioSource> ();
		mRunner = GetComponent<PlayerRunner> ();
		Game.State.OnStateChanged += StateChanged;
	}

	void Update() {
		mAnimator.SetBool("OnAir", mRunner.isOnAir);
	}
	
	void StateChanged (GameStateEnum oldState, GameStateEnum newState) {
		if (newState == GameStateEnum.GameOver) {
			mAnimator.SetTrigger ("Death");
			mAudio.Play ();
		} else if (newState == GameStateEnum.GameWon) {
			mAnimator.SetTrigger("Win");
		}
	}
}
