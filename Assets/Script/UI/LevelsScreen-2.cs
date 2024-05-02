using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsScreen : BaseScreen
{
    public Text level;

   public void Back()
    {
        ScreenManager.inst.SwitchScreen(ScreenType.Lobby);
    }
    public void Level1()
    {
        level.text = "1";
        ScreenManager.inst.SwitchScreen(ScreenType.Lobby);
    }
    public void Level2()
    {
        level.text = "2";
        ScreenManager.inst.SwitchScreen(ScreenType.Lobby);
    }
    public void Level3()
    {
        level.text = "3";
        ScreenManager.inst.SwitchScreen(ScreenType.Lobby);
    }
}
