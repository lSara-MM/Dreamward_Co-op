using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDebry : MonoBehaviour
{
    public GameObject _boss;
    public BossHealth bossState;
    public GameObject _player;
    public ParticleSystem wind;

    [Header("Bullet Attributes")]
    public GameObject bullet;
    private Animator _animator;
    public float direction = 1;

    public float bulletLife = 1f;
    public float speed = -1f;
    public float variance = 0.5f;
    [SerializeField] float scale;

    public AudioClip[] sounds;
    public AudioSource soundSource;

    [Header("Spawner Attributes")]
    [SerializeField] private float realFiringRate = 1f;


    private GameObject spawnedBullet;
    private float timer = 0f;
    private bool activeAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        //realFiringRate = 1.0f;
        _animator = _boss.GetComponent<Animator>();
        activeAttack = false;
    }

    int random;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //Activate if it was inactive and is its time to attack
        if (!activeAttack && _animator.GetInteger("ChooseAttack") == 6 + ((int)direction)) 
        {
            activeAttack = true;
            random = Random.Range(-1, 2); //If not boss phase 2 only chooses one type of wind scycthes at a time
        }

        //If atacando make wind push
        if (_animator.GetInteger("ChooseAttack") == 6 + ((int)direction)) 
        {
            if (!wind.isPlaying) { wind.Play(); }
            WindPush(bossState.bossSP);
        }
        else //if not attacking stop wind
        {
            wind.Stop();
        }

        //Shot wind sycthes
        if (timer >= realFiringRate)
        {
            soundSource.Stop();
            Fire();
            timer = 0.0f;
        }
        if(timer == 0.0f && bossState.bossSP && activeAttack) 
        {
            random = Random.Range(-1, 2);
            if (bullet && _animator.GetInteger("ChooseAttack") == 6 + ((int)direction)) 
            {
                soundSource.clip = sounds[random + 1];
                soundSource.Play();
            }
        }

        //If not longer attacking deactivate
        if (_animator.GetInteger("ChooseAttack") != 6 + ((int)direction)) 
        {
            activeAttack = false;
        }
    }


    private void Fire()
    {
        if (bullet && _animator.GetInteger("ChooseAttack") == 6+((int)direction))
        {
            Vector3 spawnPos = transform.position;
            float speedMod = 0.0f;
            float scale = 1.0f;
            
            switch (random) 
            {

                case -1:
                    {
                        
                        speedMod = 0.8f;
                        scale = 0.7f;
                        break;
                    }
                case 0:
                    {
                        speedMod = 1.0f;
                        scale = 1.0f;
                        break;
                    }
                case 1:
                    {
                        speedMod = 1.7f;
                        scale = 1.3f;
                        break;
                    }
                default: 
                    {
                        speedMod = 1.0f;
                        scale = 1.0f;
                        break;
                    }
            }

            spawnedBullet = Instantiate(bullet, spawnPos, Quaternion.identity);
            spawnedBullet.GetComponent<DebryProjectile>().speed = speed / speedMod;
            spawnedBullet.GetComponent<DebryProjectile>().bulletLife = bulletLife;
            spawnedBullet.GetComponent<DebryProjectile>().direction = direction;
            spawnedBullet.GetComponent<DebryProjectile>().scale *= scale;


            spawnedBullet.transform.rotation = transform.rotation;
        }
    }

    float delayWind = 0.0f;
    private void WindPush(bool secondPhase)
    {
        delayWind += Time.deltaTime;
        if (delayWind>= 0.05f) 
        {
            Vector3 newPosplayer;
            newPosplayer = _player.transform.position;
            if (_player.transform.localScale.x / Mathf.Abs(_player.transform.localScale.x) == direction) 
            {
                if (secondPhase) { newPosplayer.x += 0.1f * direction; }
                else { newPosplayer.x += 0.06f * direction; }
            }
            else 
            {
                if (secondPhase) {newPosplayer.x += 0.06f*direction; }
                else { newPosplayer.x += 0.02f * direction; }
                    
            }
            
            _player.transform.position = newPosplayer;
            delayWind = 0.0f;
        }
        
    }
}
