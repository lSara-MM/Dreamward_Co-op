using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPointSides;
    public Transform attackPointUp;
    public float attackRange = 0.5f;
    public int attackDamage = 50;
    public LayerMask enemyLayers;
    [SerializeField] private float _attackDelay = 2;
    private float _timer = 0;

    public AudioSource attackSound;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _attackDelay)
        {
            attackSound.Play();

            if (Input.GetMouseButtonDown(((int)MouseButton.Left)) && !Input.GetKey(KeyCode.W))
            {
                _timer = 0;
                AttackSides();
            }

            else if (Input.GetMouseButtonDown(((int)MouseButton.Left)) && Input.GetKey(KeyCode.W))
            {
                _timer = 0;
                AttackUp();
            }
        }
    }

    void AttackSides()
    {
        animator.SetTrigger("AttackSides");

        Collider2D[] hitEnemiesSides = Physics2D.OverlapCircleAll(attackPointSides.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemiesSides)
        {
            HitWhite hit = enemy.GetComponent<HitWhite>();
            BossHealth bossHit = enemy.GetComponent<BossHealth>();

            if (hit != null)
            {
                Debug.Log("We hit " + enemy.name);
                hit.DoHitWhite();
            }

            else if (bossHit != null)
            {
                Debug.Log("We hit " + enemy.name);
                bossHit.TakeDmg(attackDamage);
            }
        }
    }

    void AttackUp()
    {
        animator.SetTrigger("AttackUp");

        Collider2D[] hitEnemiesUp = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemiesUp)
        {
            HitWhite hit = enemy.GetComponent<HitWhite>();
            BossHealth bossHit = enemy.GetComponent<BossHealth>();

            if (hit != null)
            {
                Debug.Log("We hit " + enemy.name);
                hit.DoHitWhite();
            }

            else if (bossHit != null)
            {
                Debug.Log("We hit " + enemy.name);
                bossHit.TakeDmg(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointSides == null)
            return;

        if (attackPointUp == null)
            return;

        Gizmos.DrawWireSphere(attackPointSides.position, attackRange);
        Gizmos.DrawWireSphere(attackPointUp.position, attackRange);
    }
}
