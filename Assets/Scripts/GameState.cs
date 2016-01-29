using UnityEngine;
using System.Collections;
using System;

public enum GameStateEnum {
	Starting,
	Running,
	GameOver
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
		case "Starting":
			ChangeState(GameStateEnum.Starting);
			break;
		case "Running":
			ChangeState(GameStateEnum.Running);
			break;
		case "GameOver":
			ChangeState(GameStateEnum.GameOver);
			break;
		}
	}

	public void Restart() {
		Application.LoadLevel (Application.loadedLevel);
	}

	void Awake() {
		Game.State = this;
		mCurrentState = GameStateEnum.Starting;
	}

	void Start () {

	}

	void Update () {
	
	}
}
