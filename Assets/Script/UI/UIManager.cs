using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager inst;
  
    public Text enemy;
    public Text gems;
    public int points=0;
    public int enemyCount=0;
    public GameObject enemy1;

    public CameraFollow camera;
    public bool gameOver = true;
    int count = 0;
    void Start()
    {
       
        inst = this;  
    }


    public void GameOver()
    {
        ScreenManager.inst.SwitchScreen(ScreenType.GameOver);
        gameOver = true;
    }
   
    public void CameraSet()
    {
        //camera.SetPlayer();
    }

    public void Gems(int amount)
    {
        points += amount;
        gems.text = points.ToString();
    }
    public void Enemy(int amount)
    {
        enemyCount += amount;
        enemy.text = enemyCount.ToString();
        if(enemy.text == "0")
        {
            WinGame();
        }
    }

    public void ChildCount()
    {
        enemyCount = 0;

          enemy1 = GameObject.FindGameObjectWithTag("Enemy");
          enemy1 = enemy1.transform.GetChild(0).gameObject;
           
        count = enemy1.transform.childCount;
        Enemy(count);
    }
    public void WinGame()
    {
        ScreenManager.inst.SwitchScreen(ScreenType.Win);
       // Debug.Log("00");
    }

}
