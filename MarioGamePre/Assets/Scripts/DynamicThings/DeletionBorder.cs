using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletionBorder : MonoBehaviour
{
    private SoundManager mySoundManager;
    private void Awake()
    {
        mySoundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Mushroom>() != null)
        {
            Destroy(other.gameObject.transform.parent.gameObject);
            return;
        }
        if(other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            Destroy(other.gameObject);
            mySoundManager.playDeathEffectSound();
			return;
        }
    }
}
