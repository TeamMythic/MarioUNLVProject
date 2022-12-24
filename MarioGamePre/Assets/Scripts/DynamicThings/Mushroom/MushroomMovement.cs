using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class MushroomMovement : MonoBehaviour
{
    public bool isMoving = false;
	[HideInInspector] public float directionFacing = 1;
	[HideInInspector] public Rigidbody2D myRigidBody2D;
	[SerializeField] private float speed = 4f;
	private void Awake()
	{
		myRigidBody2D = this.transform.parent.GetComponent<Rigidbody2D>();
	}
	private void FixedUpdate()
	{
		if (isMoving)
		{
			myRigidBody2D.velocity = new Vector2(directionFacing * speed, myRigidBody2D.velocity.y);
		}
	}
	public void flip()
	{
		directionFacing = directionFacing * -1f;
	}
}
