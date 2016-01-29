using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIScore : MonoBehaviour {
	private Text mText;

	void Start() {
		mText = GetComponent<Text> ();
		mText.text = "0";
		Game.Score.OnScoreChanged += ScoreChanged;
	}

	void ScoreChanged (int amount) {
		mText.text = Game.Score.Score.ToString ();
	}
}
