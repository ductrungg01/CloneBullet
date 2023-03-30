using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject appearVfx;
    public EnemyMovement enemyMoving;
    public EnemyCollision enemyCollision;
    public Animator animator;
    public NavMeshAgent navMeshAgent;

    private void Start()
    {
        Appear();
    }

    private void Update()
    {
        float detectionRange = 25f;
        float distanceToPlayer = Vector3.Distance(transform.position, GameManager.Instance.playerInstance.transform.position);
        if (distanceToPlayer <= detectionRange)
        {
            Attack();
        }
    }
    

    public async UniTask Appear()
    {
        appearVfx.SetActive(true);
        enemyMoving.enabled = false;
        enemyCollision.enabled = false;
        animator.SetTrigger("Idle");
        
        await UniTask.Delay(TimeSpan.FromSeconds(3));
        
        EnemyGo();
    }

    public void EnemyGo()
    {
        appearVfx.SetActive(false);
        enemyMoving.enabled = true;
        enemyCollision.enabled = true;
        animator.SetTrigger("Walking");
    }

    async public void Dead()
    {
        navMeshAgent.SetDestination(this.transform.position);
        enemyMoving.enabled = false;
        enemyCollision.enabled = false;
        animator.SetTrigger("Dead");

        await UniTask.Delay(TimeSpan.FromSeconds(3));
        
        Destroy(this.gameObject);
    }
    
    async public void Attack()
    {
        navMeshAgent.SetDestination(this.transform.position);
        enemyMoving.enabled = false;
        enemyCollision.enabled = false;
        animator.SetTrigger("Attack");
        GameManager.Instance.playerInstance.GetComponent<Player>().BeAttacked();

        await UniTask.Delay(TimeSpan.FromSeconds(3));

        animator.SetTrigger("Idle");

    }
}
