using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveNPC : StateMachineBehaviour
{
    private BossHealth _boss;

    [SerializeField] private float speed;
    private float _timer;
    public int target = 0;
    private Vector2[] _points = { new Vector2(0, -2.16f), new Vector2(-5.78f, 2.35f), new Vector2(5.78f, 2.35f),
        new Vector2(5.78f, -2.16f), new Vector2(-5.78f, -2.16f)};//Hardcoded :)

    [SerializeField] private float _minSpeed = 0.5f;
    [SerializeField] private float _acceleration = 0.01f;

    // Check if target is slected by server
    public bool targetSelected = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss = animator.gameObject.GetComponent<BossHealth>();
        speed = 7;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (targetSelected)
        {
            if (speed >= _minSpeed && Vector2.Distance(animator.transform.position, _points[target]) < 5f)
            {
                speed -= _acceleration;
            }

            if (_boss.bossSP)
            {
                animator.transform.position = Vector2.MoveTowards(animator.transform.position, _points[target], speed * Time.deltaTime * 1.5f);
            }

            else
            {
                animator.transform.position = Vector2.MoveTowards(animator.transform.position, _points[target], speed * Time.deltaTime);
            }

            if (Vector2.Distance(animator.transform.position, _points[target]) < 0.1f)
            {
                targetSelected = false;
                animator.SetTrigger("Exit");
            }
        }
    }

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
