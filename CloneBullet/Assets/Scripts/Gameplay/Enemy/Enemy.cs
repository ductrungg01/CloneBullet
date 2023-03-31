using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject rootObject;
    public GameObject appearVfx;
    public EnemyMovement enemyMoving;
    public EnemyCollision enemyCollision;
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    public bool isBoss = false;
    public bool isDead = false;

    private void Start()
    {
        if (isBoss)
        {
            this.transform.localScale = new Vector3(1500, 1500, 1500);
        }
        
        Appear();
    }

    private void Update()
    {
        if (isDead) return;
        
        float detectionRange = 15f;
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
        isDead = true;
        navMeshAgent.SetDestination(this.transform.position);
        enemyMoving.enabled = false;
        enemyCollision.enabled = false;
        animator.SetTrigger("Dead");
        
        if (GetComponent<Enemy>().isBoss == false)
            AudioManager.Instance.PlaySoundEffect("normal-enemy-dead");
        else AudioManager.Instance.PlaySoundEffect("boss-dead");

        await UniTask.Delay(TimeSpan.FromSeconds(3));
        
        //Destroy(rootObject);
        rootObject.SetActive(false);
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
