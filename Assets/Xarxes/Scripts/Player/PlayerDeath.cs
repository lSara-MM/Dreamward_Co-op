using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public void Death()
    {
        Animator _animator = GetComponent<Animator>();
        WinLose _winLose = GameObject.Find("Game").GetComponent<WinLose>();

        // player dead
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false; // Nose si desactivo player entero se rompe
        this.gameObject.GetComponent<Collider2D>().enabled = false; // Avoid collisions after death
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

        _animator.SetTrigger("Death");

        _winLose._lost = true;
        foreach (GameObject item in Globals.dontDestroyList)
        {
            if (item.tag == "Player")
            {
                _winLose._lost = false;
                break;
            }
        }

        if (_winLose._lost)
        {
            GameObject boss = GameObject.FindWithTag("Boss").gameObject;

            if (boss != null)
            {
                Animator bossAnimator = boss.GetComponent<Animator>();
                bossAnimator.SetTrigger("BossWins");
            }
        }
    }
}
