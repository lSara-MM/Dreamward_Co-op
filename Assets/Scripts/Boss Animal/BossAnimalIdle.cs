using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimalIdle : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (animator.GetBool("Enraged")) 
        {
            //Retorna -1 (ataque unico rage) 0 (pilar)
            animator.SetInteger("ChooseAttack", Random.Range(-1, 1));
            Debug.Log("Se selecciono algo furioso:");
        }
        else 
        {
            //Retorna 0 (pilar) , 1 (laser )
            animator.SetInteger("ChooseAttack", Random.Range(0, 2));
            Debug.Log("Se selecciono algo chill:");

        }
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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