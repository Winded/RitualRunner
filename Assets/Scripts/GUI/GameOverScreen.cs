using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour {
	private GUISection mSection;

	void Start() {
		mSection = GetComponent<GUISection> ();
		Game.State.OnStateChanged += StateChanged;
	}
	
	void StateChanged (GameStateEnum oldState, GameStateEnum newState) {
		if (newState == GameStateEnum.GameOver) {
			mSection.Enable();
		}
	}
}
