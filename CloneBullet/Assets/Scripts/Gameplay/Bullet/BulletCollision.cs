using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            GetComponent<Bullet>().bounceRemain--;
            if (GetComponent<Bullet>().bounceRemain <= 0)
            {
                PoolManager.Instance.bullet.OnReturnToPool(this.gameObject);
            }

            GetComponent<Rigidbody>().velocity *= 1.5f;
        }
    }
}
