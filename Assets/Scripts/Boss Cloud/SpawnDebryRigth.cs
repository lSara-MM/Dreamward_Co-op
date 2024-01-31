using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDebryRigth : MonoBehaviour
{
    public GameObject _boss;

    [Header("Bullet Attributes")]
    public GameObject bullet;
    private Animator _animator;
    public float direction = 1;

    public float bulletLife = 1f;
    public float speed = 1f;
    public float variance = 0.5f;
    [SerializeField] float scale;


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


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= realFiringRate)
        {
            Fire();
            timer = 0;
        }
    }


    private void Fire()
    {
        if (bullet && _animator.GetInteger("ChooseAttack") == 5)
        {
            int random = Random.Range(-1, 2);
            Vector3 spawnPos = transform.position;
            float speedMod = 0.0f;
            float scale = 1.0f;
            switch (random)
            {
                case -1:
                    {
                        speedMod = 0.6f;
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
                        speedMod = 1.4f;
                        scale = 1.2f;
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
}
