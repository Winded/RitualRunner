using UnityEngine;
using System.Collections;

public class MovingEnemy : MonoBehaviour {
	public Vector3 MovementDirection = Vector3.right;
	public float MovementSpeed = 1f;

	private CharacterController2D mController;
	private bool mGrounded = false;

	void Start() {
		mController = GetComponent<CharacterController2D> ();
	}

	void Update() {
		if (!mGrounded) {
			mController.Move (Vector3.down * 1000f);
			mGrounded = true;
		}

		if (Game.State.CurrentState == GameStateEnum.Running) {
			var dir = MovementDirection * MovementSpeed * Time.deltaTime;
			mController.Move (dir);
		}
	}
}
