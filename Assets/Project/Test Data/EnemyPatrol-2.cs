using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class EnemyPatrol : MonoBehaviour
{
    public List<Transform> waypoints;
    public float waypointThreshold = 1f;
    public float patrolSpeed = 3f;
    public float waitTimeAtWaypoint = 2f;
    public float rotationSpeed = 5f;

    private NavMeshAgent navMeshAgent;
    private int currentWaypointIndex = 0;
    private bool isWaiting = false;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = patrolSpeed;
        GoToNextWaypoint();
    }

    private void Update()
    {
        if (!isWaiting && !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= waypointThreshold)
        {
            StartCoroutine(WaitAndRotateAtWaypoint());
        }
    }

    private IEnumerator WaitAndRotateAtWaypoint()
    {
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
        isWaiting = false;
    }

    private void GoToNextWaypoint()
    {
        if (waypoints.Count == 0)
            return;

        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
    }
}