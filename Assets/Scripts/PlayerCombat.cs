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
    public LayerMask enemyLayers;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(((int)MouseButton.Left)) && !Input.GetKey(KeyCode.W)) 
        {
            AttackSides();
        }
        
        if (Input.GetMouseButtonDown(((int)MouseButton.Left)) && Input.GetKey(KeyCode.W)) 
        {
            AttackUp();
        }
    }


    void AttackSides()
    {
        animator.SetTrigger("AttackSides");

        Collider2D[] hitEnemiesSides =  Physics2D.OverlapCircleAll(attackPointSides.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemiesSides) 
        {
            Debug.Log("We hit " + enemy.name);
        }
    }
    
    void AttackUp()
    {
        animator.SetTrigger("AttackUp");

        Collider2D[] hitEnemiesUp =  Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemiesUp) 
        {
            Debug.Log("We hit " + enemy.name);
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
