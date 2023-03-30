using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimTesting : MonoBehaviour
{
    KeyCode KeyCodeForIdle = KeyCode.Alpha0;
    string IDLE = "Idle";
    
    KeyCode KeyCodeForWalking = KeyCode.Alpha1;
    string WALKING = "Walking";
    
    KeyCode KeyCodeForAttack = KeyCode.Alpha2;
    string ATTACK = "Attack";
    
    KeyCode KeyCodeForDead = KeyCode.Alpha3;
    string DEAD = "Dead";
    
    public Animator anim;

    void Update()
    {
        if (Input.GetKeyDown(KeyCodeForIdle))
        {
            Debug.Log("Key " + KeyCodeForIdle + " is pressed down");
            anim.SetTrigger(IDLE);
        }
        if (Input.GetKeyDown(KeyCodeForWalking))
        {
            Debug.Log("Key " + KeyCodeForWalking + " is pressed down");
            anim.SetTrigger(WALKING);
        }
        if (Input.GetKeyDown(KeyCodeForAttack))
        {
            Debug.Log("Key " + KeyCodeForAttack + " is pressed down");
            anim.SetTrigger(ATTACK);
        }
        if (Input.GetKeyDown(KeyCodeForDead))
        {
            Debug.Log("Key " + KeyCodeForDead + " is pressed down");
            anim.SetTrigger(DEAD);
        }
    }
}
