using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinWorldSpace : MonoBehaviour
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
        StartCoroutine(collected());
    }
    private IEnumerator collected()
    {
        myStatsManager.players[0].addCoins(1);
		float localTTime = 0;
        Vector3 min = this.transform.position;
        Vector3 max = this.transform.position + new Vector3(0f, 0.5f, 0f);
        while(localTTime < 1)
        {
            this.transform.position = Vector3.Lerp(min, max, localTTime);
            mySpriteRenderer.color = Color.Lerp(new Color32(255,255,255,255), new Color(255,255,255,0), localTTime);
			localTTime += Time.deltaTime / .5f;
            yield return null;
        }
    }
}
