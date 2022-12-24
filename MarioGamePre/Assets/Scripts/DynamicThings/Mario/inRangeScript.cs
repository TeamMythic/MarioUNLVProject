using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
public class inRangeScript : MonoBehaviour
{
    private PlayerMovement myPlayerMovement;
    private playerEffects myPlayerEffects;
    private SoundManager mySoundManager;
    private GameObject flag = null;
    [SerializeField] private Animator cinimaticAnimator;
    private GameObject transitionPanels;
    private List<Image> mainPanels;
    private Camera mainCamera;
    private Volume myPostProcessing;
    private Vignette myVignette;
    private Bloom myBloom;
	private void Awake()
    {
		myPlayerMovement = this.gameObject.GetComponent<PlayerMovement>();
        myPlayerEffects = this.gameObject.GetComponent<playerEffects>();
        mySoundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
		transitionPanels = GameObject.FindGameObjectWithTag("transitionPanels");
        mainPanels = new List<Image>(transitionPanels.GetComponentsInChildren<Image>());
        mainCamera = Camera.main;
        myPostProcessing = mainCamera.gameObject.GetComponent<Volume>();
		myPostProcessing.profile.TryGet<Vignette>(out myVignette);
		myPostProcessing.profile.TryGet<Bloom>(out myBloom);
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Mushroom>() != null)
        {//Mushroom:
            if (!myPlayerMovement.isBigMarioBoolean)
            {
                myPlayerEffects.marioBig();
            }
            //Delete it:
            other.gameObject.GetComponent<Mushroom>().DeleteMe();
            return;
        }
        if (other.gameObject.GetComponent<MysteryBox>() != null)
        {//Mystery Box:
            other.gameObject.GetComponent<MysteryBox>().ObjectHit();
			myPlayerMovement.myrigidBody2D.velocity = new Vector2(myPlayerMovement.myrigidBody2D.velocity.x, -10);
            return;
		}
        if(other.gameObject.GetComponent<Brick>() != null)
        {//Brick:
            other.gameObject.GetComponent<Brick>().ObjectHit(myPlayerMovement.myAnimator.runtimeAnimatorController, myPlayerMovement.marioController);
            myPlayerMovement.myrigidBody2D.velocity = new Vector2(myPlayerMovement.myrigidBody2D.velocity.x, -10);
            return;
		}
        if(other.gameObject.CompareTag("Flag"))
        {//Hit the poll
            myPlayerMovement.myrigidBody2D.velocity = Vector2.zero;
            myPlayerMovement.enableOrDisableInput(false);
            myPlayerMovement.myrigidBody2D.isKinematic = true;
			other.enabled = false;
            StartCoroutine(mySoundManager.flagCollected());
            flag = GameObject.FindGameObjectWithTag("ActualFlag");
            cinimaticAnimator.SetTrigger("MarioSlidingDownPole");
			flag.transform.parent.GetComponent<Animator>().SetTrigger("FlagDropping");
            StartCoroutine(collectedFlag());
			return;
        }
        if(other.gameObject.CompareTag("FlagBase"))
        {//Hit the bottom of the flag base:
			other.enabled = false;
            if(flag != null)
            {
                flag.transform.parent.GetComponent<Animator>().speed = 0;//essentially pausing the animation:
                cinimaticAnimator.speed = 0;
                flag = null;
            }
			//Do some cinimatic for entering the castle or something:
		}
    }
    private IEnumerator collectedFlag()
    {//Collected the pole:
        yield return new WaitForSeconds(1.2f);
        myPlayerMovement.marioBigCollider.enabled = false;
        myPlayerMovement.marioCollider.enabled = false;
        cinimaticAnimator.speed = 1;
        cinimaticAnimator.enabled = false;
		StartCoroutine(goToCastle());
	}    
    private IEnumerator goToCastle()
    {//Animate into the castle:
        Transform castLocation = GameObject.FindGameObjectWithTag("CastleTransform").GetComponent<Transform>();
        float localTTime = 0;
        Vector3 endPosLocation = this.transform.position + new Vector3(4, -1, 0);
		Vector3 startPosLocation = this.transform.position;
        myPlayerMovement.myrigidBody2D.isKinematic = false;
        myPlayerMovement.myrigidBody2D.gravityScale = 4f;
		myPlayerMovement.marioBigCollider.enabled = true;
		myPlayerMovement.marioCollider.enabled = true;
        myPlayerMovement.overideAnimator = true;
		myPlayerMovement.myAnimator.SetBool("jumpingUp", false);
		myPlayerMovement.myAnimator.SetBool("jumpingDown", false);
		myPlayerMovement.myAnimator.SetBool("running", false);
		myPlayerMovement.myAnimator.SetBool("walking", false);
		while (localTTime < 1)
        {
			this.transform.localEulerAngles = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(0, 0, -360), localTTime);
            this.transform.position = Vector3.Lerp(startPosLocation, endPosLocation, localTTime);
			localTTime += Time.deltaTime / 1;
            yield return null;
		}
        this.transform.localEulerAngles = new Vector3(0, 0, -360);
        this.transform.position = endPosLocation;
		yield return new WaitForSeconds(.333f);
		myPlayerMovement.myAnimator.SetBool("walking", true);
		myPlayerMovement.myrigidBody2D.isKinematic = true;
		myPlayerMovement.marioBigCollider.enabled = false;
		myPlayerMovement.marioCollider.enabled = false;
        localTTime = 0;
        startPosLocation = this.transform.position;
        endPosLocation = this.transform.position + new Vector3(5.3f,0,0);
		while (localTTime < 1)
		{
			this.transform.position = Vector3.Lerp(startPosLocation, endPosLocation, localTTime);
			localTTime += Time.deltaTime / 1;
			yield return null;
		}
        this.transform.position = endPosLocation;
		myPlayerMovement.myAnimator.SetBool("walking", false);
		//Game Ends:
		localTTime = 0;
        SpriteRenderer sprite = myPlayerMovement.gameObject.GetComponent<SpriteRenderer>();
        Color32 first = new Color32(255,255,255,255);
		Color32 second = new Color32(255, 255, 255, 0);
		while (localTTime < 1)
        {
            sprite.color = Color32.Lerp(first, second, localTTime);
            localTTime += Time.deltaTime / 1f;
            yield return null;
        }
        sprite.color = second;
        localTTime = 0;
        var a = myVignette.intensity.value;
        var b = 1f;
        StartCoroutine(fadeOutBackground());
        while (localTTime < 1)
        {
            myVignette.intensity.value = Mathf.Lerp(a,b, localTTime);
            localTTime += Time.deltaTime / 2f;
            yield return null;
        }
	}
    private IEnumerator fadeOutBackground()
    {
		Color32 third = new Color32(0, 0, 0, 0);
		Color32 fourth = new Color32(0, 0, 0, 255);
		float localTTime = 0f;
        while(localTTime < 1)
        {
			mainPanels[0].color = Color32.Lerp(third, fourth, localTTime);
			localTTime += Time.deltaTime / 2f;
            yield return null;
        }
	mainPanels[0].color = fourth;
    }
}
