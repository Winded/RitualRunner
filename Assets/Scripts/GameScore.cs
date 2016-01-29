using UnityEngine;
using System.Collections;

public class GameScore : MonoBehaviour {
	public delegate void OnScoreChangedDelegate(int amount);

	public event OnScoreChangedDelegate OnScoreChanged = delegate{};

	public int Score {
		get {
			return mScore;
		}
	}

	private int mScore;

	public void AddScore(int amount) {
		mScore += amount;
		OnScoreChanged (amount);
	}

	void Awake() {
		Game.Score = this;
	}
}
