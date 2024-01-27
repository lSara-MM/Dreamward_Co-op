using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_move : StateMachineBehaviour
{
    private Boss _boss;
    //private Rigidbody2D _rb;

    [SerializeField] private float speed;
    private float _timer;
    private Vector2 target;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss = animator.GetComponent<Boss>();
        _timer = Random.Range(3, 5);
        //_rb = _boss.rb;
        target = new Vector2(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f));
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_timer <= 0)
        {
            _timer = Random.Range(3, 5);
            target = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));//todo: bounds
        }

        else
        {
            _timer -= Time.deltaTime;
        }

        animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);
        //_rb.velocity = new Vector2(speed, speed);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //_rb.velocity = new Vector2(0, 0);
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
