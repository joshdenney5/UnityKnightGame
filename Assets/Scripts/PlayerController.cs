using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using NUnit.Framework.Constraints;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private Rigidbody2D myRB;
  private SpriteRenderer myRenderer;
  private Animator myAnimator;
  private bool facingRight = true;
  private bool canMove = true;
  private bool doubleJumpPointReached;
 

  //move
  public float maxSpeed;

  //jump
  private bool grounded;
  private float groundCheckRadius = .2f;
  public LayerMask groundLayer;
  public Transform groundCheck;
  public float jumpPower;
  private bool doubleJumpEnabled = true;
  private bool doubleJumpUsed;
  private bool wallJumpEnabled = true;
  private bool wallJumpLeftUsed;
  private bool wallJumpRightUsed;
  private bool leftWallContact;
  private bool rightWallContact;
  private bool noWallContact = true;

  // Use this for initialization
  void Start()
	{
	  myRB = GetComponent<Rigidbody2D>();
	  myRenderer = GetComponent<SpriteRenderer>();
	  myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update()
	{
	  VerticalMovement();
	  CheckLayers();
    ResetAbilitiesOnGround();
	  HorizontalMovement();
	}

  private void ResetAbilitiesOnGround()
  {
    ResetDoubleJump();
    ResetWallJump();
  }

  private void CheckLayers()
  {
    grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    myAnimator.SetBool("isGrounded", grounded);
  }

  private void VerticalMovement()
  {
    Jump();
    DoubleJump();
    WallJumpLeft();
    WallJumpRight();
  }

  private void HorizontalMovement()
  {
    var move = Input.GetAxis("Horizontal");

    if (canMove)
    {
      if (move > 0 && !facingRight)
      {
        Flip();
      }
      else if (move < 0 && facingRight)
      {
        Flip();
      }
      myRB.velocity = new Vector2(move*maxSpeed, myRB.velocity.y);
      myAnimator.SetFloat("MoveSpeed", Mathf.Abs(move));
    }
    else
    {
      myRB.velocity = new Vector2(0, myRB.velocity.y);
      myAnimator.SetFloat("MoveSpeed", 0);
    }
  }

  private void ResetDoubleJump()
  {
    if (grounded)
    {
      doubleJumpUsed = false;
      doubleJumpPointReached = false;
    }
  }

  private void ResetWallJump()
  {
    if (grounded)
    {
      wallJumpLeftUsed = false;
      wallJumpRightUsed = false;
      print("Wall jump reset");
    }
  }

  private void Jump()
  {
    if (canMove && grounded && JumpInputReceived())
    {
      myAnimator.SetBool("isGrounded", false);
      ApplyJumpForce();
      grounded = false;
      print("Jump Used!");
    }
  }

  private void DoubleJump()
  {
    if (canMove && !grounded && JumpInputReceived() && DoubleJumpAvailable())
    {
      doubleJumpUsed = true;
      ApplyJumpForce();
      print("Double Jump Used!");
    }
  }

  private static bool JumpInputReceived()
  {
    return Input.GetAxis("Jump") > 0;
  }

  private void ApplyJumpForce()
  {
    myRB.velocity = new Vector2(myRB.velocity.x, 0);
    myRB.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
  }

  private void WallJumpLeft()
  {
    if (wallJumpEnabled && !grounded && JumpInputReceived() && leftWallContact && !wallJumpLeftUsed)
    {
      wallJumpLeftUsed = true;
      ApplyJumpForce();
      print("Left Wall Jump Used");
    }
  }

  private void WallJumpRight()
  {
    if (wallJumpEnabled && !grounded && JumpInputReceived() && rightWallContact && !wallJumpRightUsed)
    {
      wallJumpRightUsed = true;
      ApplyJumpForce();
      print("Right Wall Jump Used");
    }
  }

  private bool DoubleJumpAvailable()
  {
    return doubleJumpEnabled && doubleJumpPointReached && !doubleJumpUsed;
  }

  private void Flip()
  {
    facingRight = !facingRight;
    myRenderer.flipX = !myRenderer.flipX;
  }

  public void ToggleCanMove()
  {
    //canMove = !canMove;
  }

  public void DoubleJumpPointReached()
  {
    doubleJumpPointReached = true;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
   
    if (other.gameObject.CompareTag("Double Jump"))
    {
      doubleJumpEnabled = true;
      other.gameObject.SetActive(false);
    }

    if (other.gameObject.CompareTag("Wall Jump"))
    {
      wallJumpEnabled = true;
      other.gameObject.SetActive(false);
    }
  }

  void OnCollisionEnter2D(Collision2D coll)
  {
    var direction = transform.InverseTransformPoint(coll.transform.position); 

    if (direction.x > 0f && !grounded)
    {
      noWallContact = false;
      rightWallContact = true;
      print("The object collided with the right wall!");
      // leftWallContact = false;
    }
    else if (direction.x < 0f && !grounded)
    {
      noWallContact = false;
      leftWallContact = true;
      print("The object collided with the left wall!");
      //rightWallContact = false;
    }
    else
    {
      print("The object collided with nothing!");
      leftWallContact = false;
      rightWallContact = false;
    }
  }

  void OnCollisionExit2D(Collision2D coll)
  {
    noWallContact = true;
    if (wallJumpLeftUsed)
    {
      wallJumpRightUsed = false;
      print("Reset Wall Jump Right");
    }
    else if (wallJumpRightUsed)
    {
      wallJumpLeftUsed = false;
      print("Reset Wall Jump Left");
    }
  }
  
}
