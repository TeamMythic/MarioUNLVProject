using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inRangeScript : MonoBehaviour
{
    private PlayerMovement myPlayerMovement;
    private playerEffects myPlayerEffects;
	private void Awake()
    {
        myPlayerMovement = this.gameObject.GetComponent<PlayerMovement>();
        myPlayerEffects = this.gameObject.GetComponent<playerEffects>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Mushroom>() != null)
        {
            if(!myPlayerMovement.isBigMarioBoolean)
            {
			    myPlayerEffects.marioBig();
            }
            other.gameObject.GetComponent<Mushroom>().DeleteMe();
        }
        else if(other.gameObject.GetComponent<MysteryBox>() != null)
        {
            other.gameObject.GetComponent<MysteryBox>().ObjectHit();
		}
        else if(other.gameObject.GetComponent<Brick>() != null)
        {
            other.gameObject.GetComponent<Brick>().ObjectHit();
		}
    }
}
