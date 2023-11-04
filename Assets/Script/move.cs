using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class move : MonoBehaviour {
	private Rigidbody2D rb2d;
	public Rigidbody2D broomRigidBody;

	public Vector2 startingPos;
	[Range(0,2)]static public int roleID_Now = 0;
	public float moveSpeed = 8f;
	public float torquePower = 6f;
	public bool facingRight = true;

	[Header("跳躍設定")]
	public float jumpForce = 300.0f;
	public Transform footPoint;
	public Transform footPoint2;
	public bool touchGround = true;
	private bool canJump = true;
	private Coroutine cor_canJump_dead;
	public string littleRedName;
	private bool failed = false;

	public bool alternativeJumpTest;

	void Start() {
		rb2d = GetComponent<Rigidbody2D>();
		broomRigidBody.simulated = true;
		failed = false;
	}

	void Update() {
		footPointCheck();
		Jump();
		righting();
		restart();
	}
	void FixedUpdate() {
		Movement();
	}
	
	private void Movement() {		
		

		if(failed)
			return;
		
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

	private void restart(){
		if(Input.GetKey("r")) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		/*
		if(Input.GetKey("r")) {
			Time.timeScale=0;
			rb2d.position = startingPos;
			rb2d.velocity = new Vector2(0f, 0f);
			broomRigidBody.simulated = true;
			broomRigidBody.velocity = new Vector2(0f, 0f);
			broomRigidBody.angularVelocity = 0f;
			broomRigidBody.SetRotation(0f);
			failed = false;
			Time.timeScale=1;
			return;
		}
		*/
	}

	//讓傾斜的掃帚回正
	private void righting()
	{
		if(Input.GetKey("j")) {
			broomRigidBody.AddTorque(torquePower);
		}
		if(Input.GetKey("k")) {
			broomRigidBody.AddTorque(-1*torquePower);
		}
		
		if(Input.GetKey("i")) {
			if(broomRigidBody.rotation < 0f){
				broomRigidBody.AddTorque(torquePower);
			}
			else if(broomRigidBody.rotation > 0f){
				broomRigidBody.AddTorque(-1*torquePower);
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
			if(Mathf.Abs(broomRigidBody.rotation) > 90f) {
				failAndRestart();
				// rb2d.velocity = new Vector2(0f, 0f);
				// broomRigidBody.simulated = false;
				// failed = true;
			}
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
		if(failed)
			return;
		
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

	public void failAndRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
