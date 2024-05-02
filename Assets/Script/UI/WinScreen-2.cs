using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : BaseScreen
{

    public Text level;

    public void Home()
    {
        ScreenManager.inst.SwitchScreen(ScreenType.Lobby);

    }
   public void NextLevel()
    {

       
        LevelManager.inst.ChildDestroy();
        ScreenManager.inst.SwitchScreen(ScreenType.GamePlay);
      
     
        if (level.text == "1")
        {
            LevelManager.inst.Levels(LevelName.Level2);
        }
        else if (level.text == "2")
        {
            LevelManager.inst.Levels(LevelName.Level3);
        }
       
        else
        {
            LevelManager.inst.Levels(LevelName.Level1);
        }



        UIManager.inst.CameraSet();
        UIManager.inst.ChildCount();

    }

}
