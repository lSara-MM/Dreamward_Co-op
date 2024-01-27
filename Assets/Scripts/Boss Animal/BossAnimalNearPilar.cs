using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimalNearPilar : StateMachineBehaviour
{
    [SerializeField] BossBeast bossScript;
    [SerializeField] GameObject player;
    [SerializeField] GameObject nearSpawner;
    [SerializeField] GameObject farSpawner;
    private float _timer = 0f;
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
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
