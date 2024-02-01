using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateHands : MonoBehaviour
{
    [SerializeField] ParticleSystem groundTremors;
    [SerializeField] Animator idelDetection;
    public BossHealth vidaBoss; 
    public AudioSource sound;
    bool doOnce = true;

    private Animator _animator; 
    
    bool activateHands = false;
    float dtWait = 0.0f;

    bool startCountDownHands = false;
    float timerHands=0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GameObject.Find("Enemy").GetComponent<Animator>();
    }



    // Update is called once per frame
    void Update()
    {
        
            if(vidaBoss.bossSP && !activateHands && idelDetection.GetCurrentAnimatorStateInfo(0).IsName("Idle")) //Recibir un mensaje
            {
            startCountDownHands = true;
            }
            if (startCountDownHands) 
            {
                timerHands += Time.deltaTime;
                if (timerHands>0.5f ) 
                {
                    _animator.SetTrigger("Howl");
                    activateHands = true;
                    groundTremors.Play();
                    sound.Play();
                    dtWait = 0.0f;
                    startCountDownHands = false;
                }
            }
            
            if (activateHands && doOnce) 
            {
                dtWait += Time.deltaTime;

                if (dtWait > groundTremors.main.duration)
                {
                    groundTremors.Stop();
                    sound.Stop();
                    dtWait = 0.0f;
                    Vector3 pos = gameObject.transform.position;
                    pos.y += 2.0f;
                    transform.position = pos;
                    doOnce = false;

                }
            }
            
        
    }
}
