using UnityEngine;
using System.Collections;
using System;

public enum GameStateEnum {
	Running,
	GameOver,
	GameWon
}

public class GameState : MonoBehaviour {

	public delegate void OnStateChangedDelegate(GameStateEnum oldState, GameStateEnum newState);

	public event OnStateChangedDelegate OnStateChanged = delegate {};

	public GameStateEnum CurrentState {
		get {
			return mCurrentState;
		}
	}

	private GameStateEnum mCurrentState;

	public void ChangeState(GameStateEnum state) {
		var oldState = mCurrentState;
		mCurrentState = state;
		OnStateChanged (oldState, state);
	}

	public void ChangeState(string stateName) {
		switch (stateName) {
		case "Running":
			ChangeState(GameStateEnum.Running);
			break;
		case "GameOver":
			ChangeState(GameStateEnum.GameOver);
			break;
		case "GameWon":
			ChangeState(GameStateEnum.GameWon);
			break;
		}
	}

	public void Restart() {
		Application.LoadLevel (Application.loadedLevel);
	}

	void Awake() {
		Game.State = this;
		mCurrentState = GameStateEnum.Running;
	}

	void Start () {

	}

	void Update () {
	
	}
}
