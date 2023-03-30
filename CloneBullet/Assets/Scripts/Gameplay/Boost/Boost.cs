using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public GameObject rootObject;
    public int boostValue;
    public TextMeshProUGUI valueText;
    public bool isUsed = false;

    void Start()
    {
        ShowValue();
    }

    void ShowValue()
    {
        valueText.text = boostValue.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isUsed) return;

        if (other.CompareTag("Bullet"))
        {
            isUsed = true;
            CloneBullet(other.gameObject);
        }
        
        
    }

    async UniTask CloneBullet(GameObject bullet)
    {
        Vector3 velocity = bullet.GetComponent<Rigidbody>().velocity;
        
        for (int i = boostValue; i > 0; i--)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));

            GameObject newBullet = PoolManager.Instance.bullet.OnTakeFromPool(
                this.transform.position,
                Quaternion.identity);

            newBullet.GetComponent<Rigidbody>().velocity = velocity;

            boostValue--;
            ShowValue();
        }
        
        Destroy(rootObject);
    }
}