using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Components;

public class BossBothHands : StateMachineBehaviour
{
    [SerializeField] private GameObject leftHandPrefab;
    [SerializeField] private GameObject rightHandPrefab;

    private GameObject leftHandInHierarchy;
    private GameObject rightHandInHierarchy;

    private BGCcCursor _cursorLeft;
    private BGCcCursor _cursorRight;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        leftHandInHierarchy = GameObject.Find(leftHandPrefab.name).transform.Find("SweepCurve").gameObject;
        leftHandInHierarchy.SetActive(true);
        _cursorLeft = leftHandInHierarchy.GetComponent<BGCcCursor>();

        rightHandInHierarchy = GameObject.Find(rightHandPrefab.name).transform.Find("SweepCurve").gameObject;
        rightHandInHierarchy.SetActive(true);
        _cursorRight = rightHandInHierarchy.GetComponent<BGCcCursor>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_cursorLeft.DistanceRatio > 0.98f || _cursorRight.DistanceRatio > 0.98f)
        {
            animator.SetTrigger("Exit");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        leftHandInHierarchy.SetActive(false);
        _cursorLeft.DistanceRatio = 0;

        rightHandInHierarchy.SetActive(false);
        _cursorRight.DistanceRatio = 0;
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
