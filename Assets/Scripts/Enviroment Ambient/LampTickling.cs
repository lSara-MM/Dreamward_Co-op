using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class LampTickling : MonoBehaviour
{
    public Rigidbody2D rb;
    public Light2D lamp;
    float timing;
    Vector2 force = new Vector2(3,0);

    // Update is called once per frame
    void Update()
    {
        timing += Time.deltaTime;

        if(timing > Random.Range(2f, 10f)) 
        {
            lamp.enabled = false;
            timing = 0;
        }

        if(timing > Random.Range(0.2f, 0.6f) && !lamp.enabled) 
        {
            lamp.enabled  = true;
        }

        if(math.abs(rb.velocity.x) < 0.02f) 
        {
            rb.AddForce(force);
        }
    }
}
