using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimTesting : MonoBehaviour
{
    KeyCode KeyCodeForDancing = KeyCode.Alpha0;
    string DANCING = "Dancing";
    
    KeyCode KeyCodeForAiming = KeyCode.Alpha1;
    string AIMING = "Aiming";
    
    KeyCode KeyCodeForShooting = KeyCode.Alpha2;
    string SHOOTING = "Shooting";
    
    KeyCode KeyCodeForDead = KeyCode.Alpha3;
    string DEAD = "Dead";
    
    public Animator anim;

    void Update()
    {
        if (Input.GetKeyDown(KeyCodeForDancing))
        {
            Debug.Log("Key " + KeyCodeForDancing + " is pressed down");
            anim.SetTrigger(DANCING);
        }
        if (Input.GetKeyDown(KeyCodeForAiming))
        {
            Debug.Log("Key " + KeyCodeForAiming + " is pressed down");
            anim.SetTrigger(AIMING);
        }
        if (Input.GetKeyDown(KeyCodeForShooting))
        {
            Debug.Log("Key " + KeyCodeForShooting + " is pressed down");
            anim.SetTrigger(SHOOTING);
        }
        if (Input.GetKeyDown(KeyCodeForDead))
        {
            Debug.Log("Key " + KeyCodeForDead + " is pressed down");
            anim.SetTrigger(DEAD);
        }
    }
}
