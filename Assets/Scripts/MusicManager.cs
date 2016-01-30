using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	public SceneCreator SceneCreator;

	private AudioSource mAudio;

	void Awake() {
		Game.MusicManager = this;
	}

	void Start() {
		mAudio = GetComponent<AudioSource> ();
		mAudio.clip = SceneCreator.asset.clip;
		mAudio.Play ();
		Game.State.OnStateChanged += StateChanged;
	}

	void StateChanged (GameStateEnum oldState, GameStateEnum newState) {
		if(newState == GameStateEnum.GameOver) {
			mAudio.Stop();
		}
	}
}
