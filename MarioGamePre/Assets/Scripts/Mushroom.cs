using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Rendering;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private Coroutine deleteMeCo = null;
    private StatsManager myStatsManager;
    private float directionFacing = 1;
    [SerializeField] private float speed = 8f;
    private bool stopmoving;
    [SerializeField] private Transform RightCheck;
	[SerializeField] private Transform LeftCheck;
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
        this.transform.GetComponent<BoxCollider2D>().enabled = false;
        this.transform.parent.GetComponent<PolygonCollider2D>().enabled = false;
        myStatsManager.players[0].addScore(1000);
        this.transform.parent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		this.transform.parent.GetComponent<Rigidbody2D>().isKinematic = true;
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
			this.transform.parent.GetComponent<Rigidbody2D>().isKinematic = false;
			this.transform.parent.GetComponent<PolygonCollider2D>().enabled = true;
		    this.transform.parent.GetComponent<Rigidbody2D>().gravityScale = 8;
    }
}
