using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class BossCloudLightning : StateMachineBehaviour
{
    [SerializeField] private StartCouroutine _scriptCouroutine;

    [SerializeField] private GameObject _lightningPrefab;
    [SerializeField] private GameObject _player;

    [SerializeField] private float _offset = 2f;

    [Header("Particles Parameters")]
    [SerializeField] private float _psLife = 1f;
    [SerializeField] private float _psDelay = 1;

    private BossHealth _boss;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss = animator.gameObject.GetComponent<BossHealth>();
        _player = GameObject.Find("Player");
        _scriptCouroutine = GameObject.Find("Game").GetComponent<StartCouroutine>();

        _scriptCouroutine.CallCouroutine(_boss, _psDelay, new Vector3(_player.transform.position.x, -3.80f, 0), _offset);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

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
