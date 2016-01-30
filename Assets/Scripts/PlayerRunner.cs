using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerRunner : MonoBehaviour {
	public float movementSpeed = 1f;
	public Vector3 movementDirection = Vector3.right;

	public float gravityMultiplier = 1f;

	public Vector3 onGroundVerticalVelocity = Vector3.down;

	public Vector3 jumpVector = Vector3.up;
	public Vector3 dashVector = Vector3.down;

	public CameraJuice cameraJuice;

	private CharacterController2D mController;

	private bool mRunning = true;
	private bool mOnGround = false;

	private Vector3 mVerticalVelocity = Vector3.zero;

	public bool isOnAir {
		get {
			return !mOnGround;
		}
	}
	
	public void Jump() {
		if (mOnGround) {
			mVerticalVelocity = jumpVector;
			cameraJuice.DoJuicyStuff();
		}
	}

	public void Dash() {
		if (!mOnGround) {
			mVerticalVelocity = dashVector;
		}
	}

	void Start () {
		mController = GetComponent<CharacterController2D> ();
		Game.State.OnStateChanged += StateChanged;
	}

	void Update () {
		if (mRunning) {
			if(Input.GetButtonDown("Jump")) {
				Jump();
			}
			if(Input.GetButtonDown("Dash")) {
				Dash ();
			}
			RunTick();
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Enemy") {
			Game.State.ChangeState(GameStateEnum.GameOver);
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
		mRunning = newState == GameStateEnum.Running;
	}
}
