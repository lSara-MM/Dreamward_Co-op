using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class ClownHand : StateMachineBehaviour
{
    [SerializeField] private GameObject handPrefab;
    private GameObject handInHierarchy;
    private BGCcCursor _cursor;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        handInHierarchy = GameObject.Find(handPrefab.name).gameObject;
        handInHierarchy.transform.Find("Curve").gameObject.SetActive(true); //:)
        _cursor = handInHierarchy.transform.Find("Curve").GetComponent<BGCcCursor>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_cursor.DistanceRatio > 0.98f)
        {
            animator.SetTrigger("Exit");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //handPrefab.transform.GetChild(1).gameObject.SetActive(false);
        handInHierarchy.transform.Find("Curve").gameObject.SetActive(false);
        _cursor.DistanceRatio = 0;
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
