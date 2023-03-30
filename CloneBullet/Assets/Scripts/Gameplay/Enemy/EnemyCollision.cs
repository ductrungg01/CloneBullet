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
            Debug.Log("Bullet va cham Enemy");
            GetComponent<Enemy>().Dead();
        } else if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy va cham Player");
            GetComponent<Enemy>().Attack();
        }
    }
}
