using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : StateMachineBehaviour
{
    private Boss _boss;

    [SerializeField] private float speed;
    private float _timer;
    private int target = 0;
    private Vector2[] _points = { new Vector2(0, 0), new Vector2(-5.78f, 2.74f), new Vector2(6.63f, 2.57f), 
        new Vector2(5.78f, -0.72f), new Vector2(-6.08f, -0.73f)};//Hardcoded :)

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss = animator.GetComponent<Boss>();

        int aux = Random.Range(0, _points.Length);

        while(aux == target)//check to avoid the current point
        {
            aux = Random.Range(0, _points.Length);
        }

        target = aux;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, _points[target], speed * Time.deltaTime);
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
