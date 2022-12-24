using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip overworldSong;
	[SerializeField] private AudioClip overworldSongSpeedUp;
	[SerializeField] private AudioClip gameOverSoundEffect;
	[SerializeField] private AudioClip deathSoundEffect;
	[SerializeField] private AudioClip levelCompleteSoundEffect;
	private AudioSource myAudioSource;
    [HideInInspector] public Coroutine waitToPlaySpedUpSong = null;
    private void Awake()
    {
        myAudioSource = this.gameObject.GetComponent<AudioSource>();
        waitToPlaySpedUpSong = StartCoroutine(waitToPlaySpedUp());
    }
    private IEnumerator waitToPlaySpedUp()
    {
        myAudioSource.loop = true;
        yield return new WaitForSeconds(275);
        speedUpSong();
        myAudioSource.loop = false;

	}
    public void SetupLevelSong()
    {
        myAudioSource.clip = overworldSong;
        myAudioSource.Play();
    }
    public void speedUpSong()
    {
		myAudioSource.Stop();
		myAudioSource.clip = overworldSongSpeedUp;
		myAudioSource.Play();
	}
    public void playDeathEffectSound()
    {
        myAudioSource.Stop();
        myAudioSource.clip = deathSoundEffect;
        myAudioSource.Play();
    }
    public void levelComplete()
    {
		myAudioSource.Stop();
		myAudioSource.clip = levelCompleteSoundEffect;
        myAudioSource.Play();
    }
    public void GameOver()
    {
		myAudioSource.Stop();
		myAudioSource.clip = gameOverSoundEffect;
		myAudioSource.Play();
	}
}
