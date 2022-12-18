using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerEffects : MonoBehaviour
{
	Rigidbody2D myRigidbody2D;
	PlayerMovement myPlayerMovement;
	SpriteRenderer mySpriteRenderer;
	[SerializeField] private Sprite smallMario;
	[SerializeField] private Sprite bigMario;
	private void Awake()
	{
		myPlayerMovement = this.gameObject.GetComponent<PlayerMovement>();
		myRigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
		mySpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
	}
	public void marioBig()
	{
		StartCoroutine(makingMarioBig());
	}
	private IEnumerator makingMarioBig()
	{
		//Make Player still:
		myPlayerMovement.enableOrDisableInput(false);
		myRigidbody2D.isKinematic = true;
		myRigidbody2D.velocity = Vector2.zero;
		myPlayerMovement.myAnimator.runtimeAnimatorController = myPlayerMovement.marioBigController;
		yield return new WaitForSeconds(.333f);
		myPlayerMovement.myAnimator.runtimeAnimatorController = myPlayerMovement.marioController;
		yield return new WaitForSeconds(.333f);
		myPlayerMovement.myAnimator.runtimeAnimatorController = myPlayerMovement.marioBigController;
		yield return new WaitForSeconds(.333f);
		myPlayerMovement.myAnimator.runtimeAnimatorController = myPlayerMovement.marioController;
		yield return new WaitForSeconds(.333f);
		myRigidbody2D.isKinematic = false;
		myPlayerMovement.changeMarioController(true);
		myPlayerMovement.enableOrDisableInput(true);
	}
}
