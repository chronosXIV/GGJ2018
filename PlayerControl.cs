using UnityEngine;
using System;

public class PlayerControl : MonoBehaviour {

	[SerializeField]
	private Transform startPoint;

	[SerializeField]
	public AudioManager jumpSound;

	public float moveSpeed;
	public float jumpForce;

	public KeyCode left;
	public KeyCode right;
	public KeyCode jump;

	public Transform groundCheckPoint;
	public float groundCheckRadius;
	public LayerMask whatIsGround;

	public bool isGrounded;

	private Rigidbody2D theRB;
	private Animator Anim; 

	private bool FacingRight = true;

	void Start(){
			
		groundCheckPoint = transform.Find("GroundCheck");
		Anim = GetComponent<Animator>();
		theRB = GetComponent<Rigidbody2D>();

	}

	void Update(){

		isGrounded = false;

		isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

		Anim.SetBool ("Ground", isGrounded);

		Anim.SetFloat ("Speed", Mathf.Abs(theRB.velocity.x));
			
		Anim.SetFloat("vSpeed", theRB.velocity.y);

		if (Input.GetKey (left)) {

			if (FacingRight) {
				Flip ();
			}

			theRB.velocity = new Vector2 (-moveSpeed, theRB.velocity.y);

		} else if (Input.GetKey (right)) {

			if (!FacingRight) {
				Flip ();
			}

			theRB.velocity = new Vector2 (moveSpeed, theRB.velocity.y);
		
		} else {
			
			theRB.velocity = new Vector2 (0, theRB.velocity.y);

		}


		if (isGrounded && Input.GetKeyDown (jump) && Anim.GetBool ("Ground")) {

			jumpSound.Play("PlayerJump");

			// Add a vertical force to the player.

			isGrounded = false;
			Anim.SetBool ("Ground", false);
			theRB.velocity = new Vector2 (theRB.velocity.x, jumpForce);
		}

	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		FacingRight = !FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void ResetPosition() {
		this.transform.position = startPoint.position;
	}
}
