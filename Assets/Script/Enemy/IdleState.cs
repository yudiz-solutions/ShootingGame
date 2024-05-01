using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public GameObject player;
    public Rigidbody rb;

    public AttackState attackState;
    public bool isChaseRange;


    public Transform[] wayPoints;
    private int currentWayPoint = 0;
    public float speed = 1f;

    public GameObject gems;
    bool enemyDead = true;
    [SerializeField]
    Material triggeredMaterial;

    [SerializeField]
    Material normalMaterial;
    [SerializeField]
    MeshRenderer meshRenderer;


    public override State RunCurrentState()
    {
        if (isChaseRange)
        {
            isChaseRange = false;
            rb.velocity = Vector3.zero;
            meshRenderer.material = triggeredMaterial;
            return attackState;
           
        }
        else
        {
            Move();
            return this;
        }

    }

    private void Move()
    {
        Transform wp = wayPoints[currentWayPoint];

        if (Vector3.Distance(player.transform.position, wp.position) < 0.01f)
        {
           rb.MovePosition(wp.position);

            currentWayPoint = (currentWayPoint + 1) % wayPoints.Length;
            //            Debug.Log(currentWayPoint);
            meshRenderer.material = normalMaterial;
        }
        else
        {
           rb.position = Vector3.MoveTowards(player.transform.position, wp.position, speed * Time.deltaTime);
            player.transform.LookAt(wp.position);
          
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
           
            Destroy(gameObject,0.5f);
            // UIManager.inst.Enemy(-1);
           
               
          
            if (enemyDead==true)
            {
                for (int i = 0; i <= 5; i++)
                {
                   Instantiate(gems, transform.position,transform.rotation);
                }
                UIManager.inst.Enemy(-1);
                enemyDead = false;
            }
          
        }
        
    }

}
