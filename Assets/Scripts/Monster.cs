using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {
	public Transform player;
	public float yOffset;

	private float xOffset;
	private bool mChasing = true;

	void Start() {
		xOffset = (transform.position - player.position).x;
		Game.State.OnStateChanged += StateChanged;
	}

	void Update() {
		if (mChasing) {
			var yPos = Mathf.Lerp (transform.position.y, player.position.y + yOffset, 1f / 4f);
			var pos = transform.position;
			pos.x = player.position.x + xOffset;
			pos.y = yPos;
			transform.position = pos;
		}
	}

	void StateChanged (GameStateEnum oldState, GameStateEnum newState) {
		if (newState == GameStateEnum.GameOver) {
			mChasing = false;
		}
	}
}
