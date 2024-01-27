using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SecondPhase : MonoBehaviour
{
    public Light2D globalLight;
    public Light2D plattform1;
    public Light2D plattform2;
    public Light2D bossEye1;
    public Light2D bossEye2;
    public Light2D bossEye3;
    public Light2D lamp;
    public Light2D moon;

    public bool secondPhase = false;
    float timing = 0f;

    // Update is called once per frame
    void Update()
    {
        if (!secondPhase) 
        {
            globalLight.intensity = 1.74f;
            globalLight.color = new Color(148, 167, 195, 255);
        }
        else 
        {
            timing += Time.deltaTime;

            if(globalLight.intensity > 1.1f) 
            {
                globalLight.intensity -= 0.05f * timing;
            }

            if (globalLight.intensity < 1.1f)
            {
                globalLight.color = new Color(0, 0, 0, 255);
            }
        }
    }
}
