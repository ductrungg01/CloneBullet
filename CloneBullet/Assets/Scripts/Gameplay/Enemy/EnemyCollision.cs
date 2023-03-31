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
        enemy = GetComponent<Enemy>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (enemy.isDead) return;
        
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if ((enemy.isBoss && healthForBoss <= 0) || enemy.isBoss == false)
            {
                if (enemy.isBoss)
                {
                    this.transform.localScale -= new Vector3(50, 50, 50);
                }
                Debug.Log("Enemy dead");
                GetComponent<Enemy>().Dead();
            }
            healthForBoss--;
        }
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy attack player");
            GetComponent<Enemy>().Attack();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy attack player");
            GetComponent<Enemy>().Attack();
        }
    }
}
