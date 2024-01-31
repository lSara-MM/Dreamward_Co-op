using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DebryProjectile : MonoBehaviour
{
    public float bulletLife = 1f;  // Defines how long before the bullet is destroyed
    public float rotation = 0f;
    public float speed = 1f;
    public float scale = 1f;
    public float direction = 1f;

    private Vector3 spawnPoint;
    private float timer = 0f;
    void Start()
    {
        spawnPoint = transform.position;
        Vector3 newScale = transform.localScale;
        newScale.x *= scale * scale;
        newScale.y *= (scale+1.0f)/2;
        newScale.z *= scale;
        transform.localScale = newScale;
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
        // Moves right according to the bullet's rotation
        float x = timer * speed * direction;/** transform.right.x*/;
        return new Vector3(x, 0,0);
    }
}
