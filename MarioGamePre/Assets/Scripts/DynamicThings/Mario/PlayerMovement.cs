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
    [Header("Variables:")]
    public int playerId = 0;
    private float horizontal;
    public bool isFacingRight = true;
    public bool isBigMarioBoolean = false;
    private bool isJumpingUp = false;
	private bool isJumpingDown = false;
    public float speed = 6f;
    [SerializeField] private float jumpingPower = 16f;
	[HideInInspector] public bool isRunning = false;
    [HideInInspector] public bool overideAnimator = false;
    //6 and 8 are the good speeds for walking and running respectively:

    [Header("Components:")]
    public Animator myAnimator;
    public AnimatorController marioController;
    public AnimatorController marioBigController;
	[HideInInspector] public Rigidbody2D myrigidBody2D;
	[SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public BoxCollider2D marioCollider;
    public BoxCollider2D marioBigCollider;
    private BasicInput myControls;

    [Header("Transforms")]
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
        myControls.BasicInputMapping.Jump.performed += jump;
        myControls.BasicInputMapping.ReleaseJump.performed += releaseJump;
	}
    private void jump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(isGrounded())
            {
			    myrigidBody2D.velocity = new Vector2(myrigidBody2D.velocity.x, jumpingPower);
            }
		}
    }
    private void releaseJump(InputAction.CallbackContext context)
    {
		if (myrigidBody2D.velocity.y > 0f)
		{
			myrigidBody2D.velocity = new Vector2(myrigidBody2D.velocity.x, myrigidBody2D.velocity.y * 0.5f);
		}
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
                if(myAnimator.runtimeAnimatorController == marioBigController)
                {
                    speed = 12;
                    return;
                }
                speed = 10;
                return;
            }
            if(myAnimator.runtimeAnimatorController == marioBigController)
            {
                speed = 8;
                return;
            }
            speed = 6;
        }
    }
    private void setJumping(bool up, bool down)
    {
        isJumpingUp = up;
        isJumpingDown = down;
	}
	private void setAnimations(string setValue, bool type, string setValue2, bool type2)
	{
		myAnimator.SetBool(setValue, type);
		myAnimator.SetBool(setValue2, type2);
	}
	private void setAnimations(string setValue, bool type)
    {
        myAnimator.SetBool(setValue, type);
	}
    private void animationCheck()
    {
		Vector2 inputVector = myControls.BasicInputMapping.Move.ReadValue<Vector2>();
		horizontal = inputVector.x;
		if (!overideAnimator)
        { 
		    if (myrigidBody2D.velocity.y > 0.1f)
		    {//Going:
                setJumping(true, false);
		    }
		    else if (myrigidBody2D.velocity.y < -0.1f)
		    {//Going Down:
                setJumping(false, true);
		    }
		    else
		    {//we aren't moving up or down:
                setJumping(false, false);
		    }
		    if (isJumpingUp)
            {
                setAnimations("jumpingUp", true, "jumpingDown", false);
		    }
            else if(isJumpingDown)
            {
				setAnimations("jumpingUp", false, "jumpingDown", true);
		    }
            else
            {
				setAnimations("jumpingUp", false, "jumpingDown", false);
			    if (horizontal != 0)
			    {
				    if (!isRunning)
                    {
						setAnimations("walking", true, "running", false);
				    }
                    else
                    {
						setAnimations("running", true);
				    }
			    }
			    else
			    {
					setAnimations("running", false, "walking", false);
                    isRunning = false;
				    speed = 6;
			    }
		    }
        }
	}
    private void Update()
    {
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
