using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Mushroom : GenericComponentFunctions
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
        deleteMushroom();
	}
    private void deleteMushroom()
    {
        setRigidCollision(myMushroomMovement.myRigidBody2D, this.transform.GetComponent<BoxCollider2D>(), myMushroomMovement.gameObject.GetComponent<BoxCollider2D>(), true);
        myStatsManager.players[0].addScore(1000);
		myMushroomMovement.myRigidBody2D.velocity = Vector2.zero;
        myMushroomMovement.isMoving = false;
		lerpSomethingScaleParent(this.transform.localScale, new Vector3(0f, 0f, 0f), 0.5f, this.transform.gameObject);
    }
    public void callLerpUp()
    {
        lerpUp();
    }
    private void lerpUp()
    {
		lerpSomethingPositionParent(this.transform.parent.position, this.transform.parent.position + new Vector3(0, .62f, 0), 0.5f);
        setRigidCollision(myMushroomMovement.myRigidBody2D, myMushroomMovement.gameObject.GetComponent<BoxCollider2D>(), false);
		myMushroomMovement.myRigidBody2D.gravityScale = 4;
        myMushroomMovement.directionFacing = 1f;
		myMushroomMovement.isMoving = true;
    }
}
