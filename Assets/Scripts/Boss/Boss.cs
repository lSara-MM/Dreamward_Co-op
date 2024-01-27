using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Animator _animator;
    //public Rigidbody2D rb;
    public Transform playerTransform;
    //todo 2nd phase

    [Header("HP")]
    [SerializeField] private float _HP;

    void Start()
    {
        _animator = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        _HP -= damage;
        if (_HP<=0)
        {
            _animator.SetTrigger("Death");
        }
    }

    public void Death()
    {
        //hacer algo
    }
}
