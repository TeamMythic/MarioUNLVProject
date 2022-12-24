using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor.Animations;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [Header("Variables")]
    public int playerId = 0;
    private float horizontal;
    public bool isFacingRight = true;
    public bool isBigMarioBoolean = false;
    private bool isJumpingUp = false;
	private bool isJumpingDown = false;
    private bool isRunning = false;
    //6 and 8 are the good speeds for walking and running respectively:
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpingPower = 16f;
	[HideInInspector] public Rigidbody2D myrigidBody2D;
    public Animator myAnimator;
	[SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public AnimatorController marioController;
    public AnimatorController marioBigController;

    [SerializeField] private BoxCollider2D marioCollider;
    [SerializeField] private BoxCollider2D marioBigCollider;
    private BasicInput myControls;
    [SerializeField] private Transform myFireballPrefab;
    [SerializeField] private Transform throwLocation;
    private void Awake()
    {
		myControls = new BasicInput();
		myrigidBody2D = this.gameObject.GetComponent<Rigidbody2D>();
        myAnimator = this.gameObject.GetComponent<Animator>();
        enableOrDisableInput(true);
		myControls.BasicInputMapping.Running.performed += setRunning;
        myControls.BasicInputMapping.Move.performed += MovementChange;
        myControls.BasicInputMapping.Throw.performed += shootFireBall;

	}
    private void shootFireBall(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Instantiate(myFireballPrefab, throwLocation.position, throwLocation.rotation);
        }
    }
    public void enableOrDisableInput(bool value)
    {
        if(value)
        {
			myControls.BasicInputMapping.Enable();
            return;
        }
		myControls.BasicInputMapping.Disable();
    }
    private void MovementChange(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        horizontal = inputVector.x;
	}
    public void changeMarioController(bool isBigMario)
    {
        if(isBigMario)
        {
            throwLocation.transform.position += new Vector3(0, 0.25f, 0);
			jumpingPower = 19;
			isBigMarioBoolean = true;
			myAnimator.runtimeAnimatorController = marioBigController;
            marioCollider.enabled = false;
            marioBigCollider.enabled = true;
            return;
        }
		throwLocation.transform.position += new Vector3(0, -0.25f, 0);
		jumpingPower = 16;
		isBigMarioBoolean = false;
		myAnimator.runtimeAnimatorController = marioController;
        marioBigCollider.enabled = false;
        marioCollider.enabled = true;
    }
    private void setRunning(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isRunning = !isRunning;
            if(isRunning)
            {
                speed = 8;
                return;
            }
            speed = 6;
        }
    }

    private void animationCheck()
    {
		if (myrigidBody2D.velocity.y > 0.1f)
		{//Going:
			isJumpingUp = true;
			isJumpingDown = false;
		}
		else if (myrigidBody2D.velocity.y < -0.1f)
		{//Going Down:
			isJumpingUp = false;
			isJumpingDown = true;
		}
		else
		{//we aren't moving up or down:
			isJumpingDown = false;
			isJumpingUp = false;
		}

		if (isJumpingUp)
        {
			myAnimator.SetBool("jumpingUp", true);
			myAnimator.SetBool("jumpingDown", false);
		}
        else if(isJumpingDown)
        {
			myAnimator.SetBool("jumpingUp", false);
			myAnimator.SetBool("jumpingDown", true);
		}
        else
        {
			myAnimator.SetBool("jumpingUp", false);
			myAnimator.SetBool("jumpingDown", false);
			if (horizontal != 0)
			{
				if (!isRunning)
                {
					myAnimator.SetBool("walking", true);
					myAnimator.SetBool("running", false);
				}
                else
                {
					myAnimator.SetBool("running", true);
				}
			}
			else
			{
				myAnimator.SetBool("running", false);
				myAnimator.SetBool("walking", false);
                isRunning = false;
				speed = 6;
			}
		}
	}
    private void Update()
    {
		Vector2 inputVector = myControls.BasicInputMapping.Move.ReadValue<Vector2>();
		horizontal = inputVector.x;
		if (Input.GetButtonDown("Jump") && isGrounded())
        {
            myrigidBody2D.velocity = new Vector2(myrigidBody2D.velocity.x, jumpingPower);
        }
        if(Input.GetButtonUp("Jump") && myrigidBody2D.velocity.y > 0f)
        {
            myrigidBody2D.velocity = new Vector2(myrigidBody2D.velocity.x, myrigidBody2D.velocity.y * 0.5f);
        }
        animationCheck();
		Flip();
    }
    private void FixedUpdate()
    {
		myrigidBody2D.velocity = new Vector2(horizontal * speed, myrigidBody2D.velocity.y);
	}
    private bool isGrounded()
    {//Creating invisible circle at feed:
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
    }
    private void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
		}
    }

}
