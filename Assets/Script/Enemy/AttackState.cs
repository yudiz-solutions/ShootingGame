using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public Transform player;
    private float dist;
    public float moveSpeed;
    public float howClose;
    public Rigidbody rb;
    public IdleState idleState;
    public bool isAttackRange;

    public Bullet prefab;

    public Transform FirePoint;
    public float bulletSpeed = 10;

    public Animator animatorEnemy;

    bool isShooting;
    private void Start()
    {
        
    }

    public override State RunCurrentState()
    {
        if (isAttackRange)
        {
            rb.velocity = Vector3.zero;
            isAttackRange = false;
            return idleState;
        }
        else
        {
            Debug.Log("Att");
            Attack();
            return this;
        }


    }

    private void Attack()
    {
        dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist <= howClose)
        {
            transform.LookAt(player);
            //rb.AddForce(transform.forward * moveSpeed);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        if (dist >= howClose)
        {
            animatorEnemy.SetBool("Fire", false);
            isAttackRange = true;
            // isAttackRange = false;
        }
        //for Attack
        if (dist <= 6f)
        {
            animatorEnemy.SetBool("Fire", true);
            if (isShooting) return;

          //  StartCoroutine(shootRoutine());
            //Attack player
        }
    }


    
    // private IEnumerator shootRoutine()
    // {
    //     // Just in case avoid concurrent routines
    //     if (isShooting) yield break;

    //     isShooting = true;

    //     AudioManager.inst.PlayAudio(AudioName.Fire);

    //    Bullet bullet = Instantiate(prefab, FirePoint.position,transform.rotation);
    //     //  rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    //     bullet.MoveForward();
    //     Destroy(bullet.gameObject, 1f);

      

    //     yield return new WaitForSeconds(0.1f);

    //     isShooting = false;
    // }
}
