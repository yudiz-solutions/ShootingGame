using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LobbyScreen : BaseScreen
{
   
   
    public ParticleSystem round,spark1,spark2;


    public Text level;
   
  
  public void ParticalOn()
    {
       
            round.Play();
            spark1.Play();
            spark2.Play();
          
       
    }
    public void StartBtn()
    {
        
        round.Stop();
        spark1.Stop();
        spark2.Stop();
       
        ScreenManager.inst.SwitchScreen(ScreenType.GamePlay);

        LevelManager.inst.ChildDestroy();


        if (level.text == "1")
        {
            LevelManager.inst.Levels(LevelName.Level1);
        }
        else if(level.text == "2")
        {
            LevelManager.inst.Levels(LevelName.Level2);
        }
        else if (level.text == "3")
        {
            LevelManager.inst.Levels(LevelName.Level3);
        }
        else
        {
            LevelManager.inst.Levels(LevelName.Level1);
        }
   

        AudioManager.inst.PlayAudioBG(AudioNameBG.GameAudio);

        UIManager.inst.ChildCount();

        UIManager.inst.CameraSet();
    }

    public void Levels()
    {
        ScreenManager.inst.SwitchScreen(ScreenType.Levels);
    }
    public void Exit()
    {
        Application.Quit();
    }
 
}


