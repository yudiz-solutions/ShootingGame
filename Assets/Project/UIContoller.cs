using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEditor.Playables;
public class UIContoller : MonoBehaviour
{
    [SerializeField]private Button startBtn,quitBtn;    
    Canvas canvas;
    private int currentLevelIndex = 0;
       void Start()
    {
         // DontDestroyOnLoad(gameObject);
        startBtn.onClick.AddListener(StartGame);
        quitBtn.onClick.AddListener(QuitGame);
        
    }

    private void StartGame()
    {
       LoadCurrentLevel();
    }

    private void QuitGame()
    {
        Application.Quit();
    }
 public async Task LoadCurrentLevel() 
        {
            if (currentLevelIndex < 5)
            {
                await LoadSceneAsync(1, (progress) =>
                {
                    Debug.Log("Loading Progress: " + progress);
                });
            }
        }
    public  async Task LoadSceneAsync(int sceneIndex, Action<float> OnLoadSceneProgress)
    {
        var task = SceneManager.LoadSceneAsync(sceneIndex);

        while (!task.isDone)
        {
            OnLoadSceneProgress?.Invoke(task.progress);
            await Task.Yield();
        }

        return; // Add a return statement here
    }
}
