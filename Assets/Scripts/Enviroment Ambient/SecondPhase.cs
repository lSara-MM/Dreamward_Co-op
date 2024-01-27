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
    public GameObject lamp;
    public Light2D moon;

    public bool secondPhase = false;
    float timing = 0f;

    // Update is called once per frame
    void Update()
    {
        if (!secondPhase) 
        {
            globalLight.intensity = 1.32f;
            globalLight.color = new Color(0.580f, 0.654f, 0.764f, 1f);

            lamp.SetActive(false);

            bossEye1.intensity = 0.57f;
            bossEye2.intensity = 0.57f;
            bossEye3.intensity = 0.57f;

            plattform1.intensity = 0f;
            plattform2.intensity = 0f;

            moon.intensity = 0.59f;
        }
        else 
        {
            timing += Time.deltaTime;

            if(globalLight.intensity > 0f) 
            {
                globalLight.intensity -= 0.005f * timing;
            }

            if (timing > 2.8f)
            {
                globalLight.intensity = 1f;
                globalLight.color = new Color(0, 0, 0, 1); 
                timing = 0f;

                ChangeLigth();
            }
        }
    }

    void ChangeLigth() 
    {
        lamp.SetActive(true);

        bossEye1.intensity = 1.86f;
        bossEye2.intensity = 1.86f;
        bossEye3.intensity = 0.63f;

        plattform1.intensity = 1.15f;
        plattform2.intensity = 1.15f;

        moon.intensity = 2.48f;
    }
}
