using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject Particles;

    // Start is called before the first frame update
    void Start()
    {
        Particles.SetActive(true);
        rb.gravityScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.y == 0f) 
        {
            Particles.SetActive(false);
            rb.gravityScale = 4f;
        }
    }
}
