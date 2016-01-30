using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {
	public Vector3 Movement = Vector3.left;

	private bool mMoving = true;

	void Start() {
		Game.State.OnStateChanged += StateChanged;
	}

	void Update() {
		if (mMoving) {
			var pos = transform.localPosition + Movement * Time.deltaTime;
			transform.localPosition = pos;
		}
	}

	void StateChanged (GameStateEnum oldState, GameStateEnum newState) {
		if (newState != GameStateEnum.Running) {
			mMoving = false;
		}
	}
}
