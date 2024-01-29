using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneProjectile : MonoBehaviour
{
    public float bulletLife = 1f;  // Defines how long before the bullet is destroyed
    public float rotation = 0f;
    public float speed = 1f;
    public bool boomerang = false;

    private Vector3 spawnPoint;
    private float timer = 0f;
    float timeAfterBoomerang = 0f; //Para que las particulas vayan marcha atras
    void Start()
    {
        spawnPoint = transform.position;
    }
    void Update()
    {
        if (timer > bulletLife) 
        { 
            Destroy(this.gameObject);
        }
        timer += Time.deltaTime;
        //transform.position = spawnPoint + Movement(timer);
        transform.localPosition = spawnPoint + Movement(timer);
    }
    float reverse = 1.0f;
    private Vector3 Movement(float timer)
    {
        //Ir girando el hueso
        Vector3 currentEulerAngles = new Vector3(0, 0, 1) * timer * 180 * reverse;
        transform.eulerAngles = currentEulerAngles;
        //transform.localEulerAngles = currentEulerAngles;
        if (boomerang && timer > (bulletLife/2) ) 
        {
            transform.rotation.eulerAngles.Set(0, 180.0f, 0);
            boomerang = false;
            reverse = -1.0f;
            timeAfterBoomerang = bulletLife;
        }
        // Moves right according to the bullet's rotation
        float x = Mathf.Abs(timer- timeAfterBoomerang) * speed /** transform.right.x*/;
        float y = Mathf.Abs(timer- timeAfterBoomerang) * speed * 0 /** transform.right.y*/;
        return new Vector3(x, y,0);
    }
}
