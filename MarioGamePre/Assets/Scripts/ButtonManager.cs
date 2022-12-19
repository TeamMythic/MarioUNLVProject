using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : ButtonStuff
{//child:
    public StatsManager myStatsManager;
    public void Button(string value)
    {
        if(value == "Play")
        {
            PlayButtonPressed(ref myStatsManager);
            return;
        }
		if (value == "Settings")
		{
			SettingsButtonPressed();
			return;
		}
		Debug.Log("You Did Not Enter A Value For The Button");
    }
    public void QuitButton(bool warning)
    {
        QuitButtonPressed(warning);
    }
}
public class ButtonStuff : UIStructures
{//Parent:
	public void PlayButtonPressed(ref StatsManager myStatsManager)
    {//Show characters (we only have mario)
        theMainMenuObject.mainMenuHolder.SetActive(false);
		myStatsManager.startTimer();
	}
    public void SettingsButtonPressed()
    {
		theMainMenuObject.mainMenuHolder.SetActive(false);
		theSettingsMenuObject.settingsMenuHolder.SetActive(true);
    }
    public void QuitButtonPressed(bool warning)
    {
        if (warning)
        {
            theMainMenuObject.quitMenuMessage.SetActive(true);
            theMainMenuObject.panel.SetActive(true);
            return;
		}
        Application.Quit();
    }
};
public class UIStructures : MonoBehaviour
{//Grandparent:
    [System.Serializable]
    public struct mainMenu
    {
        public GameObject mainMenuHolder;
        public GameObject panel;
        public GameObject quitMenuMessage;
    };
	[System.Serializable]
	public struct settingsMenu
    {
        public GameObject settingsMenuHolder;
        public GameObject audioSettingsHolder;
        public GameObject GraphicSettingsHolder;
    };
	[System.Serializable]
	public struct pauseMenu
    {//Same as the Play menu it only has settings, back, and quit:
        public GameObject pauseMenuHolder;
    };

	public mainMenu theMainMenuObject;
	public settingsMenu theSettingsMenuObject;
	public pauseMenu thePauseMenuObject;
};
