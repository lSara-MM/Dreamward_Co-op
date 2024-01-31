using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Components;

public class BossRainHand : StateMachineBehaviour
{
    [SerializeField] private GameObject leftHandPrefab;
    [SerializeField] private GameObject rightHandPrefab;

    private GameObject leftHandInHierarchy;
    private GameObject rightHandInHierarchy;

    private BGCcCursor _cursorLeft;
    private BGCcCursor _cursorRight;

    private BGCcCursorChangeLinear _changeLinearLeft;
    private BGCcCursorChangeLinear _changeLinearRight;

    private bool _accelerate = true;
    [SerializeField] private float _maxSpeed = 15f;
    [SerializeField] private float _minSpeed = 7f;
    [SerializeField] private float _acceleration = 0.2f;

    private float _timer = 0f;
    [SerializeField] private float _delay = 1.5f;
    private float _realDelay;

    private Animator _animatorRef;

    private BossHealth _boss;
    [SerializeField] private float _maxSpeedSP = 30f;
    [SerializeField] private float _accelerationSP = 0.4f;
    [SerializeField] private float _delaySP = 0.4f;

    [SerializeField] private float _shakeIntensity = 5;
    [SerializeField] private float _shakeFrequency = 5;
    [SerializeField] private float _shakeTime = 0.5f;

    private AudioSource _clownAudio;
    public AudioClip _groundHit;

    private Animator _leftHandAnimator;
    private Animator _rightHandAnimator;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss = animator.gameObject.GetComponent<BossHealth>();
        _clownAudio = animator.gameObject.GetComponent<AudioSource>();

        leftHandInHierarchy = GameObject.Find(leftHandPrefab.name).transform.Find("RainCurve").gameObject;
        leftHandInHierarchy.SetActive(true);
        _cursorLeft = leftHandInHierarchy.GetComponent<BGCcCursor>();

        rightHandInHierarchy = GameObject.Find(rightHandPrefab.name).transform.Find("RainCurve").gameObject;
        rightHandInHierarchy.SetActive(true);
        _cursorRight = rightHandInHierarchy.GetComponent<BGCcCursor>();

        _animatorRef = animator;

        _changeLinearLeft = leftHandInHierarchy.GetComponent<BGCcCursorChangeLinear>();
        _changeLinearLeft.PointReached += PointReached;

        _changeLinearRight = rightHandInHierarchy.GetComponent<BGCcCursorChangeLinear>();

        _accelerate = false;

        if (_boss.bossSP)
        {
            _changeLinearRight.Speed = 5;
            _changeLinearLeft.Speed = 5;
        }

        else
        {
            _changeLinearRight.Speed = 3;
            _changeLinearLeft.Speed = 3;
        }

        _realDelay = _delay;
        _timer = 0f;

        _leftHandAnimator = GameObject.Find(leftHandPrefab.name).transform.Find("Hand").gameObject.GetComponent<Animator>();
        _rightHandAnimator = GameObject.Find(rightHandPrefab.name).transform.Find("Hand").gameObject.GetComponent<Animator>();
        _leftHandAnimator.SetTrigger("EnterFist");
        _rightHandAnimator.SetTrigger("EnterFist");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_boss.bossSP)
        {
            if (_accelerate && _changeLinearRight.Speed <= _maxSpeedSP && _changeLinearLeft.Speed <= _maxSpeedSP)
            {
                _changeLinearLeft.Speed += _accelerationSP;
                _changeLinearRight.Speed += _accelerationSP;
            }
        }

        else
        {
            if (_accelerate && _changeLinearRight.Speed <= _maxSpeed && _changeLinearLeft.Speed <= _maxSpeed)
            {
                _changeLinearLeft.Speed += _acceleration;
                _changeLinearRight.Speed += _acceleration;
            }
        }

        if (_changeLinearRight.Speed == 0)
        {
            _timer += Time.deltaTime;

            if (_boss.bossSP)
            {
                _realDelay = _delaySP;
            }
            else
            {
                _realDelay = _delay;
            }

            if (_timer >= _realDelay)
            {
                _accelerate = true;
            }

        }

        //if (_cursorLeft.DistanceRatio > 0.98f || _cursorRight.DistanceRatio > 0.98f)
        //{
        //    animator.SetTrigger("Exit");
        //    _leftHandAnimator.SetTrigger("Exit");
        //    _rightHandAnimator.SetTrigger("Exit");
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        leftHandInHierarchy.SetActive(false);
        _cursorLeft.DistanceRatio = 0;

        rightHandInHierarchy.SetActive(false);
        _cursorRight.DistanceRatio = 0;

        _changeLinearRight.Speed = _minSpeed;
        _changeLinearLeft.Speed = _minSpeed;
    }

    private void PointReached(object sender, BGCcCursorChangeLinear.PointReachedArgs e)
    {
        if (e.PointIndex == 1 || e.PointIndex == 4 || e.PointIndex == 6)
        {
            if (_boss.bossSP)
            {
                _changeLinearRight.Speed = _maxSpeedSP;
                _changeLinearLeft.Speed = _maxSpeedSP;
            }

            else
            {
                _changeLinearRight.Speed = _maxSpeed;
                _changeLinearLeft.Speed = _maxSpeed;
            }
        }

        else if (e.PointIndex == 3 || e.PointIndex == 5 || e.PointIndex == 7)
        {
            _timer = 0;
            _changeLinearRight.Speed = 0;
            _changeLinearLeft.Speed = 0;
            _accelerate = false;
            CameraShake.Instance.ShakeCamera(_shakeIntensity, _shakeFrequency, _shakeTime);
            _clownAudio.clip = _groundHit;
            _clownAudio.PlayDelayed(0.0f);
        }

        else if (e.PointIndex == 0)
        {
            _leftHandAnimator.SetTrigger("Exit");
            _rightHandAnimator.SetTrigger("Exit");
            _animatorRef.SetTrigger("Exit");
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
