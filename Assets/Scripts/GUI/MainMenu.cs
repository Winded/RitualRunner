using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public void PlayTutorial() {
		Application.LoadLevel (1);
	}
	
	public void PlayFirebrand() {
		Application.LoadLevel (2);
	}

	public void ExitGame() {
		Application.Quit ();
	}
}
