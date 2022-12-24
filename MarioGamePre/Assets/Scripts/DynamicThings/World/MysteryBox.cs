using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class MysteryBox : Randomizer
{
	[SerializeField] private Sprite mysteryBlockHit;
	[SerializeField] private Sprite mysterBlockNotHit;
	[SerializeField] private GameObject mushroomPrefab = null;
	[SerializeField] private Transform location = null;
	[SerializeField] private BoxCollider2D trigger;
	private Vector3 startLocation;
	private int value;
	private SpriteRenderer mySpriteRenderer;
	private void Awake()
	{
		startLocation = this.transform.position;
		mySpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
		value = randomizeMe(1,2);//4th chance to get a mushroom
	}
	public void ObjectHit()
	{
		animate();
	}
	private void animate()
	{
		trigger.enabled = false;
		lerpSomethingPositionSelf(startLocation, startLocation + new Vector3(0, .35f, 0), .1f, true, .2f);
		mySpriteRenderer.sprite = mysteryBlockHit;
		if(value == 1)
		{//1 in 6 chance to get mushroom
			var obj = Instantiate(mushroomPrefab, location.position, location.rotation);
			obj.transform.parent = null;
			obj.GetComponentInChildren<Mushroom>().callLerpUp();
		}
	}
}
