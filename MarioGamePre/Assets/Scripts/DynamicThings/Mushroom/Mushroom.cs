using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Mushroom : MonoBehaviour
{
    private Coroutine deleteMeCo = null;
    private StatsManager myStatsManager;
    [SerializeField] private MushroomMovement myMushroomMovement;
	private void Awake()
    {
        myStatsManager = GameObject.FindGameObjectWithTag("StatsManager").GetComponent<StatsManager>();
	}
    public void DeleteMe()
    {
        deleteMeCo = StartCoroutine(deleteMushroom());
    }
    private IEnumerator deleteMushroom()
    {
		myMushroomMovement.myRigidBody2D.isKinematic = true;
        this.transform.GetComponent<BoxCollider2D>().enabled = false;
		myMushroomMovement.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        myStatsManager.players[0].addScore(1000);
		myMushroomMovement.myRigidBody2D.velocity = Vector2.zero;
        myMushroomMovement.isMoving = false;
        float localTTime = 0;
        Vector3 max = this.transform.localScale;
        while(localTTime < 1)
        {
            this.transform.parent.localScale = Vector3.Lerp(max, new Vector3(0f, 0f, 0f), localTTime);
            localTTime += Time.deltaTime / .5f;
            yield return null;
        }
        Destroy(this.transform.parent.gameObject);
    }
    public void callLerpUp()
    {
        StartCoroutine(lerpUp());
    }
    private IEnumerator lerpUp()
    {
        Vector3 max = this.transform.parent.position + new Vector3(0, .62f, 0);
        Vector3 min = this.transform.parent.position;
        float localTTime = 0;
        while(localTTime < 1)
        {
            this.transform.parent.position = Vector3.Lerp(min, max, localTTime);
            localTTime += Time.deltaTime / .5f;
            yield return null;
		}
			myMushroomMovement.myRigidBody2D.isKinematic = false;
		    myMushroomMovement.myRigidBody2D.gravityScale = 4;
		    myMushroomMovement.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            myMushroomMovement.directionFacing = 1f;
		    myMushroomMovement.isMoving = true;
    }
}
