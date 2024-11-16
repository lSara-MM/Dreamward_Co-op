using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private float vertical;

    public Animator animator;
    public Transform attackPointSides;
    public Transform attackPointUp;

    public float attackRange = 0.5f;
    public int attackDamage = 50;

    [SerializeField] private Stamina stamina;
    [SerializeField] private float attackCost = 20;

    public LayerMask enemyLayers;
    [SerializeField] private float _attackDelay = 2;
    private float _timer = 0;

    public AudioSource attackSound;

    [Header("Online")]
    [SerializeField] private PlayerOnline cs_playerOnline;
    public bool isNPC = false;

    private void Start()
    {
        stamina = GetComponent<Stamina>();
        cs_playerOnline = GetComponent<PlayerOnline>();
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _attackDelay)
        {
            vertical = Input.GetAxisRaw("Vertical");
            CombatMovement();
        }
    }

    public void CombatMovement(string key = default, float key_state = 0)
    {
        if ((Input.GetButtonDown("Fire1") && !isNPC && vertical <= 0) ||
            (key == "Fire1" && key_state == 0) && isNPC)
        {
            cs_playerOnline.ManageOnlineMovement("Fire1", 0);

            _timer = 0;
            AttackSides();
        }

        else if ((Input.GetButtonDown("Fire1") && !isNPC && vertical > 0) ||
            (key == "Fire1" && key_state == 1) && isNPC)
        {
            cs_playerOnline.ManageOnlineMovement("Fire1", 1);

            _timer = 0;
            AttackUp();
        }
    }

    void AttackSides()
    {
        // TODO QG: Re-Do stamina and UI-related scripts
        //if (stamina.UseEnergy(attackCost))
        {
            attackSound.Play();
            animator.SetTrigger("AttackSides");

            Collider2D[] hitEnemiesSides = Physics2D.OverlapCircleAll(attackPointSides.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemiesSides)
            {
                Blink hit = enemy.GetComponent<Blink>();
                HitWhite hitDummy = enemy.GetComponent<HitWhite>();
                BossHealth bossHit = enemy.GetComponent<BossHealth>();

                if (hit != null)
                {
                    Debug.Log("Blanqueado" + enemy.name);
                    hit.Flash();
                }

                if (hitDummy != null)
                {
                    hitDummy.DoHitWhite();
                }

                if (bossHit != null)
                {
                    Debug.Log("Golpiado " + enemy.name);
                    bossHit.TakeDmg(attackDamage);
                }
            }
        }
    }

    void AttackUp()
    {
        // TODO QG: Re-Do stamina and UI-related scripts
        //if (stamina.UseEnergy(attackCost))
        {
            attackSound.Play();
            animator.SetTrigger("AttackUp");

            Collider2D[] hitEnemiesUp = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemiesUp)
            {
                Blink hit = enemy.GetComponent<Blink>();
                HitWhite hitDummy = enemy.GetComponent<HitWhite>();
                BossHealth bossHit = enemy.GetComponent<BossHealth>();

                if (hit != null)
                {
                    Debug.Log("Blanqueado" + enemy.name);
                    hit.Flash();
                }

                if (hitDummy != null)
                {
                    hitDummy.DoHitWhite();
                }

                if (bossHit != null)
                {
                    Debug.Log("Golpiado " + enemy.name);
                    bossHit.TakeDmg(attackDamage);
                }
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
