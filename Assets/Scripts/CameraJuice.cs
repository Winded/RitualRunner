using UnityEngine;
using System.Collections;

public class CameraJuice : MonoBehaviour {
	public float juiceSize = 5f;
	public Vector3 juicePos = Vector3.zero;

	private Camera mCamera;
	private float mOriginalSize;
	private Vector3 mOriginalPos;

	public void DoJuicyStuff() {
		mCamera.orthographicSize = juiceSize;
		transform.localPosition = juicePos;
	}

	void Start() {
		mCamera = GetComponent<Camera> ();
		mOriginalSize = mCamera.orthographicSize;
		mOriginalPos = transform.localPosition;
	}

	void Update() {
		var curFOV = mCamera.orthographicSize;
		var size = Mathf.Lerp (curFOV, mOriginalSize, 1f / 4f);
		var pos = Vector3.Lerp (transform.localPosition, mOriginalPos, 1f / 4f);
		mCamera.orthographicSize = size;
		transform.localPosition = pos;
	}
}
