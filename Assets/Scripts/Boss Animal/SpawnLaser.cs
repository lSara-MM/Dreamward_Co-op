using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLaser : MonoBehaviour
{

    [Header("Bullet Attributes")]
    public GameObject bullet;
    private Animator _animator;

    public float bulletLife = 1f;
    public float speed = 1f;
    public float variance = 0.0f;

    [Header("Spawner Attributes")]
    [SerializeField] private float realFiringRate = 1f;

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


    private void Fire()
    {
        if (bullet && _animator.GetInteger("ChooseAttack") == 5)
        {
            float random = Random.Range(-variance, variance);
            Vector3 spawnPos = transform.position;
            spawnPos.y += random;
            spawnedBullet = Instantiate(bullet, spawnPos, Quaternion.identity);
            spawnedBullet.GetComponent<BoneProjectile>().speed = speed;
            spawnedBullet.GetComponent<BoneProjectile>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }
}
