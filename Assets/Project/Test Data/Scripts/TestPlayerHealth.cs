using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEditor.Playables;
using UnityEngine.UI;
using TMPro;
public class TestPlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public int enemyCount=4;

    [SerializeField]private Canvas gameoverCanvas;
    [SerializeField] private TextMeshProUGUI gameoverText;

 [SerializeField]private TextMeshProUGUI healthText;
    private void Start()
    {
        currentHealth = maxHealth;
        Health((int)currentHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        Debug.Log("Player Health: " + currentHealth);
        Health((int)currentHealth);
        if (currentHealth <= 0f)
        {
            Die();
        }
    }
private void Health(int health)
{
    healthText.text = "Health: " + health;
}
   public void EnemyKilled()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            gameoverText.text = "You Win!";
            gameoverCanvas.enabled=true;
            Invoke("Restart",5f);
        }
    }
    private void Die()
    {
         gameoverText.text = "You Loose!";
            gameoverCanvas.enabled=true;
        Invoke("Restart",5f);
    }
    public void Restart()
    {
        LoadCurrentLevel();
    }
     public async Task LoadCurrentLevel() 
        {
            if (0 < 5)
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