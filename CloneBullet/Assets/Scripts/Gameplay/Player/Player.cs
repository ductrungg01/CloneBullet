using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    public bool isDead = false;

    public async void BeAttacked()
    {
        if (isDead) return;
        anim.SetTrigger("Dead");
        isDead = true;

        AudioManager.Instance.PlaySoundEffect("player-dead");

        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        
        AudioManager.Instance.PlaySoundEffect("game-over");
    }
}