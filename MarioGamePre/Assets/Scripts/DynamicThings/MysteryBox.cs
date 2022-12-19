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
	private int value;
	private SpriteRenderer mySpriteRenderer;
	private void Awake()
	{
		mySpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
		value = randomizeMe(1,2);
	}
	public void ObjectHit()
	{
		StartCoroutine(animate());
	}
	private IEnumerator animate()
	{
		trigger.enabled = false;
		float localTTime = 0f;
		Vector3 min = transform.position;
		Vector3 max = transform.position + new Vector3(0, .35f, 0);
		while (localTTime < 1)
		{
			this.transform.position = Vector3.Lerp(min, max, localTTime);
			localTTime += Time.deltaTime / .1f;
			yield return null;
		}
		localTTime = 0f;
		while (localTTime < 1)
		{
			this.transform.position = Vector3.Lerp(max, min, localTTime);
			localTTime += Time.deltaTime / .2f;
			yield return null;
		}
		mySpriteRenderer.sprite = mysteryBlockHit;
		if(value == 1)
		{//1 in 6 chance to get mushroom
			var obj = Instantiate(mushroomPrefab, location.position, location.rotation);
			obj.transform.parent = null;
			obj.GetComponentInChildren<Mushroom>().callLerpUp();
		}
	}
}
