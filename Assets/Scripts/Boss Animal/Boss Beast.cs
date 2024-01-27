using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeast : MonoBehaviour
{
    private Animator _animator;
    public Transform playerTransform;
    public bool bossSP = false;// Boss second phase

    [Header("HP")]
    [SerializeField] private float _MaxHP = 4000f;
    [SerializeField] private float _HP;

    void Start()
    {
        _animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _HP = _MaxHP;
    }

    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        _HP -= damage;

        if (_HP <= 0)
        {
            _animator.SetTrigger("Death");
            Death();
            Debug.Log("Beast Dead");
        }

        else if (_HP <= _MaxHP / 2 && bossSP == false)
        {
            bossSP = true;
            _animator.SetBool("Enraged", bossSP);
            Debug.Log("Enraged true");
        }
    }

    public void Death()
    {
        //hacer algo
    }
}
