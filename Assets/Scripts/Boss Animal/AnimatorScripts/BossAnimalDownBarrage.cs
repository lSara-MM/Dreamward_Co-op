using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimalDownBarrage : StateMachineBehaviour
{
    private float _timer = 0f;
    [SerializeField] private float delay = 1.0f;
    GameObject spawner;
    private AudioSource _audioSource;
    public AudioClip _audioClip;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spawner = GameObject.Find("BeastLateralSpawner");
        spawner.transform.transform.position += new Vector3(0, -0.5f,0); 
        _timer = 0f;
        _audioSource = animator.gameObject.GetComponent<AudioSource>();
        _audioSource.clip = _audioClip;
        _audioSource.loop = true;
        _audioSource.PlayDelayed(0);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer += Time.deltaTime;

        if (_timer >= delay)
        {
            animator.SetInteger("ChooseAttack", 5);//hardcoded, when == 5 shoot
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("ChooseAttack", 12);//Stop shooting
        spawner.transform.transform.position += new Vector3(0, 0.5f, 0);
        _audioSource.Stop();
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
