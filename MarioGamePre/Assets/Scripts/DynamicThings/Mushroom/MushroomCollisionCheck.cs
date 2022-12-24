using Autodesk.Fbx;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MushroomCollisionCheck : MonoBehaviour
{
    [SerializeField] private MushroomMovement myMushroomMovementScript;
    private void OnCollisionEnter2D(Collision2D other)
    {
		if(other.gameObject.layer == 6)//the ground layer
		{
			myMushroomMovementScript.flip();
			return;
		}
	}
}
