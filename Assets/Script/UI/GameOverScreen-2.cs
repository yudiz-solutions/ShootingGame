using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : BaseScreen
{
    public Button home,reTry;

    public void Home()
    {

        ScreenManager.inst.SwitchScreen(ScreenType.Lobby);
    }
    public void Retry()
    {
        ScreenManager.inst.SwitchScreen(ScreenType.GamePlay);
    }
   
}
