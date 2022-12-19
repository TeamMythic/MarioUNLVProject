using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MushroomMovement : MonoBehaviour
{
    public bool isMoving = false;
	private float directionFacing = 1;
	[HideInInspector] public Rigidbody2D myRigidBody2D;
	[SerializeField] private float speed = 4f;
	private bool delayAfterFlip = false;
	private void Awake()
	{
		myRigidBody2D = this.gameObject.GetComponent<Rigidbody2D>();
	}
	private void FixedUpdate()
	{
		if (isMoving)
		{
			myRigidBody2D.velocity = new Vector2(directionFacing * speed, myRigidBody2D.velocity.y);
		}
	}
	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.layer == 6)
		{
			Debug.Log("ground");
		}
	}
}
