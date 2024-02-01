using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : StateMachineBehaviour
{
    public float wait = 1.0f;
    public float realWait = 0.0f;
    [SerializeField] float idleTime = 0.0f;
    [SerializeField] int moves = 3;
    [SerializeField] private int lastCommand = 9;
    private BossHealth _Boss;
    public Vector3[] positions; //Positions at where the cloud boss will move
    int posToMove = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _Boss = animator.gameObject.GetComponent<BossHealth>();
        if (_Boss.bossSP) { idleTime = 0.0f; }
            realWait = wait;
        posToMove++; //Cambiar a la posicion de la isla que ir
        if (posToMove == positions.Length) { posToMove = 0; } //Si se salio del rango del vector ponerlo a 0 de nuevo
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_Boss.bossSP)
        {
            moves = 5;
        }

        idleTime += Time.deltaTime;

        animator.transform.position = Vector3.MoveTowards(animator.transform.position, positions[posToMove], 2 * Time.deltaTime);

        if (idleTime >= realWait)
        {
            //Lateral attack D (0), lateral attack I (1), Strong Rain (2)

            int randNum = Random.Range(0, moves);

            while (animator.GetInteger("ChooseAttack") == lastCommand) 
            {
                animator.SetInteger("ChooseAttack", randNum);
                if
            }
            
            lastCommand = animator.GetInteger("ChooseAttack");


            idleTime = 0.0f;
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
