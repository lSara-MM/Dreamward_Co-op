using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin }


    [Header("Bullet Attributes")]
    public GameObject bullet;
    private Animator _animator;

    public float bulletLife = 1f;
    public float speed = 1f;


    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;


    private GameObject spawnedBullet;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInParent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (spawnerType == SpawnerType.Spin) transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
        if (timer >= firingRate)
        {
            Fire();
            timer = 0;
        }
    }


    private void Fire()
    {
        if (bullet && _animator.GetInteger("ChooseAttack") == 1)
        {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<Bullet>().speed = speed;
            spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }

}
