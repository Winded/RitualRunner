using UnityEngine;
using System.Collections;

public class GameWonScreen : MonoBehaviour {
	private GUISection mSection;
	
	void Start() {
		mSection = GetComponent<GUISection> ();
		Game.State.OnStateChanged += StateChanged;
	}
	
	void StateChanged (GameStateEnum oldState, GameStateEnum newState) {
		if (newState == GameStateEnum.GameWon) {
			mSection.Enable();
		}
	}
}
