using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour {
	private GUISection mSection;

	void Start() {
		mSection = GetComponent<GUISection> ();
		Game.State.OnStateChanged += StateChanged;
	}

	void Update() {
		if (mSection.isEnabled && Input.GetKeyDown (KeyCode.R)) {
			Game.State.Restart();
		}
	}
	
	void StateChanged (GameStateEnum oldState, GameStateEnum newState) {
		if (newState == GameStateEnum.GameOver) {
			mSection.Enable();
		}
	}
}
