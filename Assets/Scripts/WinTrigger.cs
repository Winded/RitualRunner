﻿using UnityEngine;
using System.Collections;

public class WinTrigger : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Game.State.ChangeState(GameStateEnum.GameWon);
		}
	}
}
