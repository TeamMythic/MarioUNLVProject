using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinWorldSpace : LerpMacros
{
    private SpriteRenderer mySpriteRenderer;
    private StatsManager myStatsManager;
    private void Awake()
    {
        myStatsManager = GameObject.FindGameObjectWithTag("StatsManager").GetComponent<StatsManager>();
        mySpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }
    public void callCollectedEffect()
    {
        collected();
    }
    private void collected()
    {
        myStatsManager.players[0].addCoins(1);
        lerpSomethingPositionSelf(this.transform.position, this.transform.position + new Vector3(0f, 0.5f, 0f), .5f, this.gameObject);
        lerpSomethingColor(new Color32(255, 255, 255, 255), new Color(255, 255, 255, 0), .35f, this.gameObject, mySpriteRenderer);
    }
}
