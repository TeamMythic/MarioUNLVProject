using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

public class Brick : Randomizer
{
	[SerializeField] private GameObject coinPrefab = null;
	[SerializeField] private Transform location = null;
	[SerializeField] private BoxCollider2D trigger;
	[SerializeField] private Transform particleEffect;
	private int amountOfCoins = 0;
	private SpriteRenderer mySpriteRenderer;
	private Vector3 StartLocation;
	private void Awake()
	{
		StartLocation = this.transform.position;
		mySpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
		if(randomizeMe(0, 3) >= 2)// 3rd chance
		{//Then it contains a coin:
			amountOfCoins = randomizeMe(1, 4);//1 - 3
		}
		else
		{
			amountOfCoins = 0;
		}
	}
	public void ObjectHit(RuntimeAnimatorController marioAnimator, AnimatorController smallMarioController)
	{
		StartCoroutine(animate(marioAnimator, smallMarioController));
	}
	private IEnumerator animate(RuntimeAnimatorController marioAnimator, AnimatorController smallMarioController)
	{
		if(marioAnimator != smallMarioController)
		{
			if(amountOfCoins <= 0)
			{
				trigger.enabled = false;
			}
			float localTTime = 0f;
			Vector3 min = StartLocation;
			Vector3 max = StartLocation + new Vector3(0, .35f, 0);
			while (localTTime < 1)
			{
				this.transform.position = Vector3.Lerp(min, max, localTTime);
				localTTime += Time.deltaTime / .1f;
				yield return null;
			}
			if(amountOfCoins > 0)
			{
				var obj = Instantiate(coinPrefab, location.position, location.rotation);
				obj.GetComponent<CoinWorldSpace>().callCollectedEffect();
				localTTime = 0f;
				while (localTTime < 1)
				{
					this.transform.position = Vector3.Lerp(max, min, localTTime);
					localTTime += Time.deltaTime / .1f;
					yield return null;
				}
				amountOfCoins--;
			}
			else
			{
				Instantiate(particleEffect, this.transform.position, this.transform.rotation);
				Destroy(this.gameObject);
			}
		}
		else
		{
			float localTTime = 0f;
			Vector3 min = StartLocation;
			Vector3 max = StartLocation + new Vector3(0, .35f, 0);
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
				localTTime += Time.deltaTime / .1f;
				yield return null;
			}
		}
	}
}
