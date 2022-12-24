using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericComponentFunctions : LerpMacros
{
	public void setRigidCollision(Rigidbody2D rb, BoxCollider2D one, bool type)
	{
		if(type)
		{
			rb.isKinematic = true;
			one.enabled = false;
			return;
		}
		rb.isKinematic = false;
		one.enabled = true;
	}
	public void setRigidCollision(Rigidbody2D rb, BoxCollider2D one, BoxCollider2D two, bool type)
	{
		if (type)
		{
			rb.isKinematic = true;
			one.enabled = false;
			two.enabled = false;
			return;
		}
		rb.isKinematic = false;
		one.enabled = true;
		two.enabled = true;
	}
}
