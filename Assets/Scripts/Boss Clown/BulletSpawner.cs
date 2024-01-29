using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { STRAIGHT, SPIN }


    [Header("Bullet Attributes")]
    public GameObject bullet;
    private Animator _animator;
    private BossHealth _boss;

    public float bulletLife = 1f;
    public float speed = 1f;


    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] float firingRate = 1f;//keep reference when 2nd phase
    private float realFiringRate = 1f;

    private GameObject spawnedBullet;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        realFiringRate = firingRate;
        _animator = GetComponentInParent<Animator>();
        _boss = GetComponentInParent<BossHealth>();
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (_boss.bossSP)
        {
            realFiringRate = firingRate * 0.80f;//increase fire rate
        }
        else
        {
            realFiringRate = firingRate;
        }

        if (spawnerType == SpawnerType.SPIN)
        {
            transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
        }
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
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            if (_boss.bossSP)
            {
                spawnedBullet.GetComponent<Bullet>().speed = speed * 2;
            }
            else
            {
                spawnedBullet.GetComponent<Bullet>().speed = speed;
            }
            spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }
}
