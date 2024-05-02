using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PatrollingState : IEnemyState
{
    private EnemyStateMachine stateMachine;
    private RoamingNPC enemyAI;
    private NavMeshAgent navMeshAgent;
    public float patrolSpeed = 2f;
    public float waypointThreshold = 1f;
    
    //private bool isWaiting = false;

    public PatrollingState(EnemyStateMachine stateMachine, RoamingNPC enemyAI)
    {
        this.stateMachine = stateMachine;
        this.enemyAI = enemyAI;
        navMeshAgent = enemyAI.GetComponent<NavMeshAgent>();
    }

    public void Enter()
    {
        navMeshAgent.speed = patrolSpeed;
        enemyAI.GoToNextWaypoint();
    }

    public void Update()
    {
        if (!enemyAI.isWaiting && !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= waypointThreshold)
        {
            enemyAI.WaitAndRotate();
        }

        if (Vector3.Distance(enemyAI.transform.position, enemyAI.playerTransform.position) <= enemyAI.attackRadius)
        {
            stateMachine.TransitionToState(EnemyState.Attacking);
        }
    }

    public void Exit()
    {
        // Perform any necessary cleanup or transitions when exiting the patrolling state
    }
}