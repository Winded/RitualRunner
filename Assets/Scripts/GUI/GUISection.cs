using UnityEngine;
using System.Collections;

public class GUISection : MonoBehaviour {
	public bool initiallyEnabled = false;

	private CanvasGroup mCanvasGroup;

	public void Enable() {
		mCanvasGroup.alpha = 1f;
		mCanvasGroup.interactable = true;
		mCanvasGroup.blocksRaycasts = true;
	}

	public void Disable() {
		mCanvasGroup.alpha = 0f;
		mCanvasGroup.interactable = false;
		mCanvasGroup.blocksRaycasts = false;
	}

	void Start() {
		mCanvasGroup = GetComponent<CanvasGroup> ();
		if (initiallyEnabled)
			Enable ();
		else
			Disable ();
	}
}
