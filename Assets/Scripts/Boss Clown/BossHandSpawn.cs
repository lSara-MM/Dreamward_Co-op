using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandSpawn : StateMachineBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float _delay = 5;
    private float _timer;
    public Vector2 target = new Vector2(0, 0);

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);

        if (Vector2.Distance(animator.transform.position, target) < 0.0001f)
        {
            _timer += Time.deltaTime;
            //TODO QG: play audio

            if (_timer >= _delay)
            {
                _timer = 0;
                animator.SetTrigger("Exit");
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
