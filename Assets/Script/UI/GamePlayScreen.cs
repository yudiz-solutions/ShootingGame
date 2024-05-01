using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayScreen : BaseScreen
{

   // public Button pause;
    public Canvas pauseCanvas;


    public void PauseBtn()
    {
        pauseCanvas.enabled = true;
        Time.timeScale = 0;
        //ScreenManager.inst.SwitchScreen(ScreenType.Pause);
    }
    public void ResumeBtn()
    {
        pauseCanvas.enabled = false;
        Time.timeScale = 1;

    }
    public void Home()
    {
        pauseCanvas.enabled = false;
        Time.timeScale = 1;
        ScreenManager.inst.SwitchScreen(ScreenType.Lobby);
    }


}
