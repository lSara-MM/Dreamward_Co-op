using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLaserEnrage : MonoBehaviour
{

    [Header("Bullet Attributes")]
    public GameObject bullet;
    private Animator _animator;

    public float bulletLife = 1f;
    public float speed = 1f;

    [Header("Spawner Attributes")]
    [SerializeField] private float realFiringRate = 1.4f;

    private GameObject spawnedBullet;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //realFiringRate = 1.0f;
        _animator = GetComponentInParent<Animator>();
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

    bool addHeigth = true;
    private void Fire()
    {
        if (bullet && _animator.GetInteger("ChooseAttack") == 6)
        {
            //bool addHeigth = (Random.value > 0.5f);
            addHeigth = !addHeigth;
            Vector3 spawnPos = transform.position;
            if (addHeigth)
            {
                spawnPos.y += 2.0f;
            }
            spawnedBullet = Instantiate(bullet, spawnPos, Quaternion.identity);
            spawnedBullet.GetComponent<BoneProjectile>().speed = speed;
            spawnedBullet.GetComponent<BoneProjectile>().bulletLife = bulletLife;
            spawnedBullet.GetComponent<BoneProjectile>().boomerang = (Random.value > 0.3f);
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }
}
