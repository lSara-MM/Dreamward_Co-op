using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownAudio : MonoBehaviour
{
    private AudioSource _audioSource;
    private Animator _animator;
    public AudioClip _winLaugh;
    public AudioClip _bulletLaugh;
    private bool _playOnce;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _playOnce = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((_animator.GetCurrentAnimatorStateInfo(0).IsName("BossWin") || _animator.GetCurrentAnimatorStateInfo(0).IsName("Death")) && _playOnce)
        {
            _audioSource.clip = _winLaugh;
            _audioSource.PlayDelayed(0);
            _playOnce = false;
        }

        else if (_animator.GetCurrentAnimatorStateInfo(0).IsName("BulletHell") && _playOnce)
        {
            _audioSource.clip = _bulletLaugh;
            _audioSource.PlayDelayed(0);
            _playOnce = false;
        }

        else if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            _playOnce = true;
        }
    }
}
