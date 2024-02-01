using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimalPilarAttack : StateMachineBehaviour
{
    [SerializeField] BossBeast bossScript;
    [SerializeField] GameObject player;
    [SerializeField] GameObject nearSpawner;
    [SerializeField] GameObject farSpawner;
    private float _timer = 0f;

    [SerializeField] private int repeat = -1;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        nearSpawner = GameObject.Find("PilarAttackSpawnerNear");
        farSpawner = GameObject.Find("PilarAttackSpawnerFar");
        _timer = 0f;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer += Time.deltaTime;

        if (_timer >= 1.0f)
        {
            animator.SetInteger("ChooseAttack", 0); //No se como va bien esto
            if (player.transform.position.x > 0) 
            {
                SpawnColumn spawnNear = nearSpawner.GetComponent<SpawnColumn>();
                spawnNear.spawn = true;
            }
            else 
            {
                SpawnColumn spawnFar = farSpawner.GetComponent<SpawnColumn>();
                spawnFar.spawn = true;
            }
            
            
            _timer = 0.0f;
            //Repetir ataque
            if (repeat == -1)
            {
                if (animator.GetBool("Enraged"))
                {
                    repeat = Random.Range(1, 4);
                }
                else
                {
                    repeat = Random.Range(0, 2);
                }
            }

        }
    }

    
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (repeat >= 0)
        {
            repeat--;
            animator.SetInteger("ChooseAttack", 10);

            if (repeat > 0)
            {
                animator.SetInteger("ChooseAttack", 0);
            }
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
