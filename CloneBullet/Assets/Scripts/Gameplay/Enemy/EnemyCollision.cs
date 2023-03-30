using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Enemy dead");
            GetComponent<Enemy>().Dead();
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
