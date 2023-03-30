using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public LineRenderer lightOfSight;
    public Animator anim;
    public Transform bulletPosStart;

    public Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    async void Update()
    {
        if (player.isDead) return;
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue))
        {
            // Rotate the player
            RotatePlayer(hit);

            if (Input.GetButtonDown("Fire1")) // Aiming
            {
                // enable the light of sight
                lightOfSight.enabled = true;
                
                anim.SetTrigger("Aiming");
            }
            else if (Input.GetButtonUp("Fire1")) // Shooting
            {
                // disable light of sight
                lightOfSight.enabled = false;
                
                anim.SetTrigger("Shooting");
                
                // Spawn the bullet and shoot
                SpawnBullet(hit.point);

                await UniTask.Delay(TimeSpan.FromSeconds(1.5));
                anim.SetTrigger("Dancing");
            }
        }
    }

    void SpawnBullet(Vector3 hitPoint)
    {
        AudioManager.Instance.PlaySoundEffect("gun-shooting");
        
        Vector3 bulletDir = hitPoint - this.transform.position;
        bulletDir.y = 0;

        GameObject bullet = PoolManager.Instance.bullet.OnTakeFromPool(
            bulletPosStart.position,
            Quaternion.identity);
        bullet.GetComponent<Bullet>().bounceRemain = ConfigurationUtil.BulletBounce;
        
        PoolManager.Instance.bullet.OnReturnToPool(bullet, 15f);

        // Set bullet's velocity
        bullet.GetComponentInChildren<Rigidbody>().velocity = bulletDir.normalized * ConfigurationUtil.BulletSpeed;
    }
    
    void RotatePlayer(RaycastHit hit)
    {
        var lookDir = hit.point - transform.position;
        float angle = Mathf.Atan2(lookDir.x, lookDir.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, angle, transform.rotation.z));
    }
}
