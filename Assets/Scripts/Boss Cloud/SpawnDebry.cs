using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDebry : MonoBehaviour
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

    enum heigth 
    {
        LOW, 
        MEDIUM,
        HIGH,
    }
    [SerializeField] heigth actualH = heigth.LOW;

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
        if (bullet && (_animator.GetInteger("ChooseAttack") == 5 || _animator.GetInteger("ChooseAttack") == 6))
        {
            int random = Random.Range(-1, 2);
            Vector3 spawnPos = transform.position;
            switch (actualH) 
            {
                case heigth.LOW:
                    {
                        actualH = heigth.MEDIUM;
                        spawnPos.y += -2.5f;
                        break;
                    }
                case heigth.MEDIUM:
                    {
                        actualH = heigth.HIGH;
                        spawnPos.y += 0.0f;
                        break;
                    }
                case heigth.HIGH: 
                    {
                        actualH = heigth.LOW;
                        spawnPos.y += 2.5f;
                        break;
                    }
            }
            spawnedBullet = Instantiate(bullet, spawnPos, Quaternion.identity);
            spawnedBullet.GetComponent<DebryProjectile>().speed = speed/ (1 + variance * random);
            spawnedBullet.GetComponent<DebryProjectile>().bulletLife = bulletLife;
            spawnedBullet.GetComponent<DebryProjectile>().direction = direction;
            spawnedBullet.GetComponent<DebryProjectile>().scale *= (1.0f + variance * random);


            spawnedBullet.transform.rotation = transform.rotation;
        }
    }
}
