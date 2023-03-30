using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject appearVfx;
    public EnemyMovement enemyMoving;
    public EnemyCollision enemyCollision;
    public Animator animator;

    private void Start()
    {
        Appear();
    }

    public async UniTask Appear()
    {
        appearVfx.SetActive(true);
        enemyMoving.enabled = false;
        enemyCollision.enabled = false;
        animator.SetTrigger("Idle");
        
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        
        EnemyGo();
    }

    public void EnemyGo()
    {
        appearVfx.SetActive(false);
        enemyMoving.enabled = true;
        enemyCollision.enabled = true;
        animator.SetTrigger("Walking");
    }
}
