using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AttackingState : IEnemyState
{
    private EnemyStateMachine stateMachine;
    private RoamingNPC enemyAI;
    private NavMeshAgent navMeshAgent;
    private bool isAttacking = false;
    private float lastAttackTime;
    private float navMeshStopDistance = 1f;

    public AttackingState(EnemyStateMachine stateMachine, RoamingNPC enemyAI)
    {
        this.stateMachine = stateMachine;
        this.enemyAI = enemyAI;
        navMeshAgent = enemyAI.GetComponent<NavMeshAgent>();
    }

    public void Enter()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = enemyAI.chaseSpeed;
        isAttacking = false;
        lastAttackTime = Time.time;
    }

    public void Update()
    {
        if (Vector3.Distance(enemyAI.transform.position, enemyAI.playerTransform.position) > enemyAI.attackRadius)
        {
            stateMachine.TransitionToState(EnemyState.Patrolling);
        }
        else
        {
            Vector3 directionToPlayer = enemyAI.playerTransform.position - enemyAI.transform.position;
            Vector3 attackPosition;

            RaycastHit hit;
            if (Physics.Raycast(enemyAI.transform.position, directionToPlayer, out hit, enemyAI.attackDistance))
            {
                attackPosition = hit.point;
            }
            else
            {
                attackPosition = enemyAI.playerTransform.position;
            }

            float distanceToPlayer = Vector3.Distance(enemyAI.transform.position, enemyAI.playerTransform.position);

            if (distanceToPlayer > navMeshStopDistance)
            {
                navMeshAgent.SetDestination(attackPosition);
            }
            else
            {
                navMeshAgent.SetDestination(enemyAI.transform.position);
            }

            if (!isAttacking && distanceToPlayer <= enemyAI.attackDistance)
            {
                enemyAI.TriggerAttackAnimation(true);
                isAttacking = true;
                // enemyAI.navMeshAgent.Stop();
            }

            if (isAttacking && Time.time - lastAttackTime >= enemyAI.attackCooldown)
            {
                enemyAI.AttackPlayer();
                lastAttackTime = Time.time;
            }

            if (isAttacking && distanceToPlayer > enemyAI.attackDistance)
            {
                enemyAI.TriggerAttackAnimation(false);
                enemyAI.TriggerPatrollingAnimation();
                isAttacking = false;
                // enemyAI.navMeshAgent.Resume();
            }
        }
    }


    public void Exit()
    {
        isAttacking = false;
    }
}