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
            GetComponent<Enemy>().Dead();
        } else if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Enemy>().Attack();
        }
    }
}
