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
            //Retorna -1 (ataque unico rage) 0 (pilar cercano) y 1 (pilar lejano)
            animator.SetInteger("ChooseAttack", Random.Range(-1, 2));
            Debug.Log("Se selecciono algo furioso:");
        }
        else 
        {
            //Retorna 0 (pilar cercano) , 1 (pilar lejano) , 2 (laser inferior) o 3 (laser superior)
            animator.SetInteger("ChooseAttack", Random.Range(0, 4));
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