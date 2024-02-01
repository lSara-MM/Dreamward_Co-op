using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCloudLateral : StateMachineBehaviour
{
    [SerializeField] float _timer = 0f;
    [SerializeField] private float delay = 1.0f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer = 0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer += Time.deltaTime;

        if (_timer >= delay && 4>animator.GetInteger("ChooseAttack"))
        {
            if(animator.GetInteger("ChooseAttack") == 0)
            {
                animator.SetInteger("ChooseAttack", 5);//hardcoded, when == 5 shoot RIGTH
            }
            else if (animator.GetInteger("ChooseAttack") == 1) 
            {
                animator.SetInteger("ChooseAttack", 7);//hardcoded, when == 6 shoot LEFT
            }
            
            
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetInteger("ChooseAttack") == 5)
        {

            animator.SetInteger("ChooseAttack", 10);//Stop shooting
        }
        else 
        {
            animator.SetInteger("ChooseAttack", 11);//Stop shooting
        }
        
        _timer = 0.0f;
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
