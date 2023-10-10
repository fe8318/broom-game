using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {
	private Rigidbody2D rb2d;
	public Rigidbody2D broomRigidBody;

	public Vector2 startingPos;
	[Range(0,2)]static public int roleID_Now = 0;
	public float moveSpeed = 8f;
	public bool facingRight = true;

	[Header("跳躍設定")]
	public float jumpForce = 300.0f;
	public Transform footPoint;
	public Transform footPoint2;
	public bool touchGround = true;
	private bool canJump = true;
	private Coroutine cor_canJump_dead;
	public string littleRedName;

	public bool alternativeJumpTest;

	void Start() {
		rb2d = GetComponent<Rigidbody2D>();
	}

	void Update() {
		footPointCheck();
		Jump();
	}
	void FixedUpdate() {
		Movement();
	}
	
	private void Movement() {
		if(Input.GetKey("r")) {
			rb2d.position = startingPos;
			rb2d.velocity = new Vector2(0f, 0f);
			broomRigidBody.velocity = new Vector2(0f, 0f);
			broomRigidBody.angularVelocity = 0f;
			broomRigidBody.SetRotation(0f);
			return;
		}

		if(touchGround) {
			rb2d.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rb2d.velocity.y);
		} else {
			rb2d.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal") * 0.85f, rb2d.velocity.y);
		}

		if(Input.GetAxis("Horizontal") != 0f) {
			if(!(facingRight ^ (Input.GetAxis("Horizontal") < 0f))) {
				Flip();
			}
		}
	}

	private void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void footPointCheck() {
		if(Physics2D.OverlapCircle(footPoint.position, 0.15f, LayerMask.GetMask("Ground & Wall") | LayerMask.GetMask("Platform")) == true || 
			Physics2D.OverlapCircle(footPoint2.position, 0.15f, LayerMask.GetMask("Ground & Wall") | LayerMask.GetMask("Platform")) == true) {
			touchGround = true;
		} else {
			touchGround = false;
		}

		//touchGround = Physics2D.OverlapCircle(footPoint.position, 0.15f, LayerMask.GetMask("Ground & Wall") | LayerMask.GetMask("Platform"));
		if(touchGround == true) {
			canJump = true;
			if(cor_canJump_dead != null)
				StopCoroutine(cor_canJump_dead);
		} else {
			if(cor_canJump_dead == null)
				cor_canJump_dead = StartCoroutine(canJump_dead());
		}
	}

	private IEnumerator canJump_dead() {
		yield return new WaitForSeconds(0.1f);
		canJump = false;
	}

	private void Jump() {
		if(Input.GetButtonDown("Jump") && canJump && touchGround) {
			canJump = false;
			if(alternativeJumpTest) {
				float jumpAngle = broomRigidBody.rotation/180f*Mathf.PI + Mathf.PI/2;
				rb2d.AddForce(new Vector2(Mathf.Cos(jumpAngle), Mathf.Sin(jumpAngle)) * jumpForce);
			} else {
				rb2d.AddForce(Vector2.up * jumpForce);
			}
		}
	}
}
