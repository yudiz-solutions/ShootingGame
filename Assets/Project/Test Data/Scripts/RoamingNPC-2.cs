using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class RoamingNPC : MonoBehaviour
{
    public Transform[] waypoints;
    public float attackRadius = 5f;
    public float attackDistance = 2f;
    public float attackDamage = 10f;
    public float attackCooldown = 1f;
    public float attackStopDistance = 0.1f;
    public Transform playerTransform;
    public float health = 100f;
    public float chaseSpeed = 5f;
    public float rotationSpeed = 10f;
    public float waitTimeAtWaypoint;
    public bool isWaiting;
    public Animator animator;
    public NavMeshAgent navMeshAgent;

    private EnemyStateMachine stateMachine;
    private int currentWaypointIndex = 0;

    private void Awake()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
    }

    void Update()
    {
        float speed = navMeshAgent.velocity.magnitude;

        animator.SetFloat("Speed", speed);
    }

    public void StartDeath()
    {
        navMeshAgent.isStopped = true;
        Destroy(gameObject, 2f);
    }

    public void AttackPlayer()
    {
        Debug.Log("Attacking player!");

        TestPlayerHealth playerHealth = playerTransform.GetComponent<TestPlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }

        StartCoroutine(ResetAttackState());
    }

    private IEnumerator ResetAttackState()
    {
        yield return new WaitForSeconds(attackCooldown);
        // Perform any necessary actions after the attack cooldown
    }

private void OnTriggerEnter(Collider other) {
    if(other.CompareTag("Bullet"))
    {
        TakeDamage(50);
        Debug.Log("Enemy hit by bullet");
    }
}
    public void TakeDamage(float damage)
    {
        if (stateMachine.CurrentState == EnemyState.Death)
        {
            return;
        }

        health -= damage;
        if (health <= 0)
        {
               TestPlayerHealth playerHealth = playerTransform.GetComponent<TestPlayerHealth>();
               playerHealth.EnemyKilled();
            stateMachine.TransitionToState(EnemyState.Death);
        }
    }
    
    public void TriggerAttackAnimation(bool isAttacking)
    {
        animator.SetBool("Attack",isAttacking);
    }

    public void TriggerPatrollingAnimation()
    {
        animator.SetTrigger("Patrol");
    }
    
    public void TriggerIdleAnimation()
    {
        animator.SetTrigger("Idle");
    }

    public void TriggerDeathAnimation()
    {
        animator.SetTrigger("Die");
    }

    public void WaitAndRotate()
    {
        StartCoroutine(WaitAndRotateAtWaypoint());
    }
    
    private IEnumerator WaitAndRotateAtWaypoint()
    {
        TriggerIdleAnimation();
        isWaiting = true;
        yield return new WaitForSeconds(waitTimeAtWaypoint);

        Vector3 targetDirection = waypoints[currentWaypointIndex].position - transform.position;
        targetDirection.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        while (transform.rotation != targetRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        GoToNextWaypoint();
        TriggerPatrollingAnimation();
        isWaiting = false;
    }

    public void GoToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    private void OnDrawGizmosSelected()
    {
        if (stateMachine != null && stateMachine.CurrentState != EnemyState.Death)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRadius);

            Vector3 hemisphereCenter = transform.position;
            hemisphereCenter.y += attackRadius * 0.5f;

            Gizmos.DrawWireSphere(hemisphereCenter, attackRadius);

            Vector3 startPoint = transform.position;
            Vector3 endPoint = hemisphereCenter;
            Gizmos.DrawLine(startPoint, endPoint);
        }
    }
}