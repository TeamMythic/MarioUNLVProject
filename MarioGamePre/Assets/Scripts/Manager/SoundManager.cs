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
    [SerializeField] private AudioClip flagCollectedSoundEffect;
	private AudioSource myAudioSource;
    [HideInInspector] public Coroutine waitToPlaySpedUpSong = null;
    private void Awake()
    {
        myAudioSource = this.gameObject.GetComponent<AudioSource>();
    }
    private IEnumerator waitToPlaySpedUp()
    {
        yield return new WaitForSeconds(275);
        speedUpSong();
	}
    public void SetupLevelSong()
    {
		myAudioSource.loop = true;
		myAudioSource.clip = overworldSong;
        myAudioSource.Play();
		waitToPlaySpedUpSong = StartCoroutine(waitToPlaySpedUp());
	}
    public void speedUpSong()
    {
		myAudioSource.Stop();
		myAudioSource.clip = overworldSongSpeedUp;
		myAudioSource.Play();
	}
    public void playDeathEffectSound()
    {
		myAudioSource.loop = false;
		myAudioSource.Stop();
        myAudioSource.clip = deathSoundEffect;
        myAudioSource.Play();
    }
    public void levelComplete()
    {
		myAudioSource.loop = false;
		myAudioSource.Stop();
		myAudioSource.clip = levelCompleteSoundEffect;
        myAudioSource.Play();
    }
    public IEnumerator flagCollected()
    {
		myAudioSource.loop = false;
		myAudioSource.Stop();
        myAudioSource.clip = flagCollectedSoundEffect;
        myAudioSource.Play();
        yield return new WaitForSeconds(2f);
        levelComplete();
	}
    public void GameOver()
    {
        myAudioSource.loop = false;
		myAudioSource.Stop();
		myAudioSource.clip = gameOverSoundEffect;
		myAudioSource.Play();
	}
}
