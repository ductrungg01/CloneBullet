using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private Enemy enemy;
    private int healthForBoss = 10;

    private void Start()
    {
        enemy = this.gameObject.GetComponent<Enemy>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if ((enemy.isBoss && healthForBoss <= 0) || enemy.isBoss == false)
            {
                if (enemy.isBoss)
                {
                    this.transform.localScale -= new Vector3(50, 50, 50);
                }
                GetComponent<Enemy>().Dead();
            }
            healthForBoss--;
        }
        
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Enemy>().Attack();
        }
    }
}
