using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCloudBigBolts : StateMachineBehaviour
{
    [SerializeField] private StartCouroutine _scriptCouroutine;

    [SerializeField] private GameObject _bigBoltsPrefab;
    [SerializeField] private int _loops = 1;

    [SerializeField] private float _offset = 2f;
    [SerializeField] private float _psDelay = 1;

    private BossHealth _boss;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss = animator.gameObject.GetComponent<BossHealth>();
        _scriptCouroutine = GameObject.Find("Game").GetComponent<StartCouroutine>();

        _scriptCouroutine.CallBigBoltsCouroutine(_bigBoltsPrefab, _boss, _psDelay, Vector3.zero, _offset, _loops);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("ChooseAttack", 11); //Poner a ningun ataque
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
