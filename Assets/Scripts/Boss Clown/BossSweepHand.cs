using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class BossSweepHand : StateMachineBehaviour
{
    [SerializeField] private GameObject handPrefab;
    private GameObject handInHierarchy;
    private BGCcCursor _cursor;

    private BGCcCursorChangeLinear _changeLinear;
    private bool _accelerate = true;
    [SerializeField] private float _maxSpeed = 15f;
    [SerializeField] private float _minSpeed = 7f;
    [SerializeField] private float _acceleration = 0.2f;
    private Animator _animatorRef;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        handInHierarchy = GameObject.Find(handPrefab.name).gameObject;
        handInHierarchy.transform.Find("SweepCurve").gameObject.SetActive(true); //:)
        _animatorRef = animator;
        _cursor = handInHierarchy.transform.Find("SweepCurve").GetComponent<BGCcCursor>();
        _changeLinear = handInHierarchy.transform.Find("SweepCurve").GetComponent<BGCcCursorChangeLinear>();
        _changeLinear.PointReached += PointReached;
        _accelerate = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_accelerate && _changeLinear.Speed <= _maxSpeed)
        {
            _changeLinear.Speed += _acceleration;
        }

        else if (!_accelerate && _changeLinear.Speed >= _minSpeed)
        {
            _changeLinear.Speed -= _acceleration;
        }

        //if (_cursor.DistanceRatio > 0.98f)
        //{
        //    animator.SetTrigger("Exit");
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //handPrefab.transform.GetChild(1).gameObject.SetActive(false);
        handInHierarchy.transform.Find("SweepCurve").gameObject.SetActive(false);
        _cursor.DistanceRatio = 0;
        _changeLinear.PointReached -= PointReached;
    }
    private void PointReached(object sender, BGCcCursorChangeLinear.PointReachedArgs e)
    {
        if (e.PointIndex == 0)
        {
            _accelerate = true;
            _animatorRef.SetTrigger("Exit");
        }

        else if (e.PointIndex == 4)
        {
            _accelerate = false;
        }
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
