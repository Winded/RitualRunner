using UnityEngine;
using System.Collections;

public class CameraJuice : MonoBehaviour {
	public float juiceSize = 5f;

	private Camera mCamera;
	private float mOriginalSize;

	public void DoJuicyStuff() {
		mCamera.orthographicSize = juiceSize;
	}

	void Start() {
		mCamera = GetComponent<Camera> ();
		mOriginalSize = mCamera.orthographicSize;
	}

	void Update() {
		var curFOV = mCamera.orthographicSize;
		var size = Mathf.Lerp (curFOV, mOriginalSize, 1f / 4f);
		mCamera.orthographicSize = size;
	}
}
