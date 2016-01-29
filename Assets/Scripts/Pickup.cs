using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	public int scoreReward;

	void Start() {

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Game.Score.AddScore (scoreReward);
			Destroy (gameObject);
		}
	}
}
