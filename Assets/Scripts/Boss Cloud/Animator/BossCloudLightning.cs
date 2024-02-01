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
    [SerializeField] private float _psDelay = 1.0f;

    [SerializeField] private AudioSource _sound;

    private BossHealth _boss;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        _boss = animator.gameObject.GetComponent<BossHealth>();
        _player = GameObject.Find("Player");
        _scriptCouroutine = GameObject.Find("Game").GetComponent<StartCouroutine>();
        if (_sound) _sound.Play();
        _scriptCouroutine.CallLightningCouroutine(_lightningPrefab, _boss, _psDelay, new Vector3(_player.transform.position.x, -3.80f, 0), _offset);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("ChooseAttack", 13); //Ningun ataque
        if (_sound) { _sound.Stop(); }
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
