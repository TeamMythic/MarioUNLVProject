using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEditor;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private string WorldLevel;
	[SerializeField] private int timeRemaining;
	[SerializeField] private TextMeshPro timeRemainingTxt;
	[SerializeField] private TextMeshPro worldLevelTxt;
	[SerializeField] private TextMeshPro playerTxt;
	[SerializeField] private TextMeshPro scoreTxt;
	[SerializeField] private TextMeshPro coinCountTxt;
    [System.Serializable]
    public class playerData
    {
        private string playerSelected;
		private int coinCount;
		private int score;
        public playerData(string playerSelected, int coinCount, int score)
        {
            this.playerSelected = playerSelected;
            this.coinCount = coinCount;
            this.score = score;
        }
        public void addScore(int amount)
        {
            score += amount;
        }
        public void addCoins(int amount)
        {
            coinCount += amount;
        }
        public int getCoinCount()
        {
            return coinCount;
        }
		public int getScore()
        {
            return score;
        }
        public string getPlayerSelected()
        {
            return playerSelected;
        }
    };
    public List<playerData> players;
    private void Awake()
    {
        playerData player1 = new playerData("Mario", 0, 0);
		players.Add(player1);
    }
    private void Update()
    {
		timeRemainingTxt.SetText(timeRemaining.ToString("000"));
		worldLevelTxt.SetText(WorldLevel);
        playerTxt.SetText(players[0].getPlayerSelected());
        scoreTxt.SetText(players[0].getScore().ToString("000000"));
        coinCountTxt.SetText("X" + players[0].getCoinCount().ToString("00"));
	}
    public void startTimer()
    {
        StartCoroutine(timeremainingGoDown());
    }
    private IEnumerator timeremainingGoDown()
    {
        while(timeRemaining > 0)
        {
            yield return new WaitForSeconds(1);
            timeRemaining--;
        }
    }
}
