using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LampTickling : MonoBehaviour
{
    public Light2D lamp;
    float timing;

    // Update is called once per frame
    void Update()
    {
        timing += Time.deltaTime;

        if(timing > Random.RandomRange(2f, 10f)) 
        {
            lamp.enabled = false;
            timing = 0;
        }

        if(timing > 0.2f && !lamp.enabled) 
        {
            lamp.enabled  = true;
        }
    }
}
