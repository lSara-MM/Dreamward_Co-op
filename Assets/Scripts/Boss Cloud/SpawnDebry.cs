using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDebry : MonoBehaviour
{
    public GameObject _boss;
    public BossHealth bossState;
    public GameObject _player;

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
    // Start is called before the first frame update
    void Start()
    {
        //realFiringRate = 1.0f;
        _animator = _boss.GetComponent<Animator>();
    }

    int random;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (bossState.bossSP && _animator.GetInteger("ChooseAttack") == 6 + ((int)direction)) 
        {
            WindPush();
        }
        if (timer >= realFiringRate)
        {
            soundSource.Stop();
            Fire();
            timer = 0.0f;
        }
        if(timer == 0.0f) 
        {
            random = Random.Range(-1, 2);
            if (bullet && _animator.GetInteger("ChooseAttack") == 6 + ((int)direction)) 
            {
                soundSource.clip = sounds[random + 1];
                soundSource.Play();
            }
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
                        
                        speedMod = 0.7f;
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
                        speedMod = 1.5f;
                        scale = 1.3f;
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

    private void WindPush()
    {
    
    }
}
