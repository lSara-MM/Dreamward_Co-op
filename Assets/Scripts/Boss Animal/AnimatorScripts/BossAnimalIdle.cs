using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimalIdle : StateMachineBehaviour
{

    public float wait = 1.0f;
    public float realWait = 0.0f;
    [SerializeField] float timeDT = 0.0f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.SetInteger("ChooseAttack", -10); //No hacer nada

        timeDT = 0.0f;
        realWait = wait; //Sumar valores random y/o dividir o restar si esta enranged

        if (animator.GetBool("Enraged")) 
        {
            //Retorna -1 (ataque unico rage) 0 (pilar)
            animator.SetInteger("ChooseAttack", Random.Range(-1, 1));
            Debug.Log("Se selecciono algo furioso:");
        }
        else 
        {
            //Retorna 0 (pilar) , 1 (laser down) , 2 (laser up)
            animator.SetInteger("ChooseAttack", Random.Range(0, 3));
            Debug.Log("Se selecciono algo chill:");

        }
        
    }

    
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeDT += Time.deltaTime;
        if(timeDT >= realWait) 
        {
            if (animator.GetBool("Enraged"))
            {
                //Retorna -1 (ataque unico rage) 0 (pilar)
                animator.SetInteger("ChooseAttack", Random.Range(-1, 1));
                Debug.Log("Se selecciono algo furioso:");
            }
            else
            {
                //Retorna 0 (pilar) , 1 (laser down) , 2 (laser up)
                animator.SetInteger("ChooseAttack", Random.Range(0, 3));
                Debug.Log("Se selecciono algo chill:");

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