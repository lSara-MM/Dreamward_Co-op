using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseHand : StateMachineBehaviour
{
    private Transform _player;
    [SerializeField] private float _speed;
    private float _timer = 0;

    [SerializeField] private float _delay = 0.01f;
    [SerializeField] private float _delaySP = 0.01f;

    private float _fallTimer = 0;
    [SerializeField] private float _delayFall = 0.01f;
    [SerializeField] private float _delayFallSP = 0.01f;

    private Vector2 _landingPos = new Vector2(0, 0);
    [SerializeField] private GameObject handPrefab;
    private Transform _handTransform;
    private Vector2 _startingPoint;
    private bool _goStart;
    private BossHealth _boss;

    [SerializeField] private float _shakeHand = 0.5f;
    [SerializeField] private float _shakeIntensity = 5;
    [SerializeField] private float _shakeFrequency = 5;
    [SerializeField] private float _shakeTime = 0.5f;

    private AudioSource _clownAudio;
    public AudioClip _groundHit;

    private Animator _handAnimator;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss = animator.gameObject.GetComponent<BossHealth>();
        _clownAudio = animator.gameObject.GetComponent<AudioSource>();

        _player = GameObject.Find("Player Online Version").transform; // Change name so that it chases new Player online
        _speed = 10;
        _landingPos = new Vector2(999,999);
        _timer = 0;
        _fallTimer = 0;
        _handTransform = GameObject.Find(handPrefab.name).transform.Find("Hand");
        _startingPoint = new Vector2(_handTransform.position.x, _handTransform.position.y);
        _goStart = false;

        _handAnimator = GameObject.Find(handPrefab.name).transform.Find("Hand").gameObject.GetComponent<Animator>();
        _handAnimator.SetBool("Fisting", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_boss.bossSP)
        {
            _delay = _delaySP;
            _delayFall = _delayFallSP;
        }

        if (_timer >= _delay)
        {
            if (_fallTimer >= _delayFall)
            {
                if (!_goStart)
                {
                    _handTransform.position = Vector2.MoveTowards(_handTransform.position, _landingPos, _speed * Time.deltaTime * 2);

                    if (Vector2.Distance(_handTransform.position, _landingPos) < 0.1f)
                    {
                        CameraShake.Instance.ShakeCamera(_shakeIntensity, _shakeFrequency, _shakeTime);
                        _clownAudio.clip = _groundHit;
                        _clownAudio.PlayDelayed(0.0f);
                        _goStart = true;
                    }
                }

                else if (_goStart)
                {
                    _handTransform.position = Vector2.MoveTowards(_handTransform.position, _startingPoint, _speed * 0.75f * Time.deltaTime);

                    if (Vector2.Distance(_handTransform.position, _startingPoint) < 0.1f)
                    {
                        _handAnimator.SetBool("Fisting", false);
                        animator.SetTrigger("Exit");
                    }
                }
            }

            else
            {
                if (_landingPos == new Vector2(999, 999))// choose where to land
                {
                    _landingPos = new Vector2(_player.position.x, -3.2f);
                }

                _fallTimer += Time.deltaTime;
                float x = Random.Range(-1, 2) * _shakeHand;
                float y = Random.Range(-1, 2) * _shakeHand;
                _handTransform.position = new Vector3(_handTransform.position.x + x, _handTransform.position.y + y, _handTransform.position.z);
            }

        }

        else
        {
            _timer += Time.deltaTime;
            _handTransform.position = Vector2.MoveTowards(_handTransform.position,
                new Vector2(_player.position.x, 2.65f), _speed * Time.deltaTime);
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
