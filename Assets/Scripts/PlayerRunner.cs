using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerRunner : MonoBehaviour {
	public float movementSpeed = 1f;
	public Vector3 movementDirection = Vector3.right;

	public float gravityMultiplier = 1f;

	public Vector3 onGroundVerticalVelocity = Vector3.down;

	public Vector3 jumpVector = Vector3.up;

	private CharacterController2D mController;

	private bool mRunning = false;
	private bool mOnGround = false;

	private Vector3 mVerticalVelocity = Vector3.zero;
	
	public void Jump() {
		mVerticalVelocity = jumpVector;
	}

	void Start () {
		mController = GetComponent<CharacterController2D> ();
		Game.State.OnStateChanged += StateChanged;
	}

	void Update () {
		if (mRunning) {
			if(mOnGround && Input.GetButtonDown("Jump")) {
				Jump();
			}
			RunTick();
		}
	}

	void RunTick() {
		mVerticalVelocity += (Vector3)Physics2D.gravity * gravityMultiplier * Time.deltaTime;

		var moveVec = movementDirection * movementSpeed * Time.deltaTime + mVerticalVelocity;
		var flags = mController.Move (moveVec);

		if ((flags & CharacterController2D.CollisionFlags.Bottom) == CharacterController2D.CollisionFlags.Bottom) {
			mVerticalVelocity = onGroundVerticalVelocity;
			mOnGround = true;
		} else {
			mOnGround = false;
		}

		if ((flags & CharacterController2D.CollisionFlags.Right) == CharacterController2D.CollisionFlags.Right) {
			Game.State.ChangeState(GameStateEnum.GameOver);
		}
	}

	void StateChanged (GameStateEnum oldState, GameStateEnum newState) {
		if (newState == GameStateEnum.GameOver) {
			print ("YOU DUN GOOFED");
			mRunning = false;
		} else if (newState == GameStateEnum.Running) {
			mRunning = true;
		}
	}
}
