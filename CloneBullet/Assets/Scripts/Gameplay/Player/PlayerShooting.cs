using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public LineRenderer lightOfSight;
    public Animator anim;
    void Update()
    {
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
            }
        }
    }

    void RotatePlayer(RaycastHit hit)
    {
        var lookDir = hit.point - transform.position;
        float angle = Mathf.Atan2(lookDir.x, lookDir.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, angle, transform.rotation.z));
    }
}
