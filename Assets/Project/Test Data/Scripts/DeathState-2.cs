using System.Collections;
using UnityEngine;

public class DeathState : IEnemyState
{
    private EnemyStateMachine stateMachine;
    private RoamingNPC enemyAI;

    public DeathState(EnemyStateMachine stateMachine, RoamingNPC enemyAI)
    {
        this.stateMachine = stateMachine;
        this.enemyAI = enemyAI;
    }

    public void Enter()
    {
        enemyAI.StartDeath();
        enemyAI.TriggerDeathAnimation();
        enemyAI.StartCoroutine(DeactivateEnemyAfterDelay());
    }

    public void Update()
    {
        // No updates needed in the death state
    }

    public void Exit()
    {
        // No cleanup needed when exiting the death state
    }
    
    private IEnumerator DeactivateEnemyAfterDelay()
    {
        yield return new WaitForSeconds(15f);
        enemyAI.gameObject.SetActive(false);
    }
}