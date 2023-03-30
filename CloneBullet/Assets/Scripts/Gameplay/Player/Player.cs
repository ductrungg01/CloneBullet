using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    private bool isDead = false;
    public void BeAttacked()
    {
        if (isDead) return;
        anim.SetTrigger("Dead");
        isDead = true;
    }
}
