using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SecondPhaseBoss3 : MonoBehaviour
{
    public Light2D globalLight;
    public Light2D bossEye1;

    public Light2D Lamp1;
    public Light2D Lamp2;

    public GameObject Lightings;

    [SerializeField] private BossHealth bossHealth;

    float timing = 0f;

    bool isInSecondFase = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.LeftShift))
        {
            bossHealth.bossSP = true;
        }

        if (!bossHealth.bossSP) 
        {
            globalLight.intensity = 2.41f;
            globalLight.color = new Color(0.580f, 0.654f, 0.764f, 1f);

            Lamp1.intensity = 1f;
            Lamp1.color = new Color(0.717f, 0.559f, 0.152f, 1f);
            
            Lamp2.intensity = 1f;
            Lamp2.color = new Color(0.717f, 0.559f, 0.152f, 1f);

            bossEye1.intensity = 0.57f;

            Lightings.SetActive(false);

            timing = 0f;
        }
        else 
        {
            if (!isInSecondFase)
            {
                timing += Time.deltaTime;

                if (globalLight.intensity > 0f)
                {
                    globalLight.intensity -= 0.005f * timing;
                }

                if (timing > 2.8f)
                {
                    globalLight.intensity = 1.5f;
                    globalLight.color = new Color(0.580f, 0.654f, 0.764f, 1);
                    timing = 0f;

                    ChangeLigth();

                    isInSecondFase = true;
                }
            }
        }
    }

    void ChangeLigth()
    {
        Lightings.SetActive(true);

        Lamp1.intensity = 5f;
        Lamp1.color = new Color(0.213f, 0.034f, 0.273f, 1f);

        Lamp2.intensity = 5f;
        Lamp2.color = new Color(0.213f, 0.034f, 0.273f, 1f);

        bossEye1.intensity = 1.86f;
    }
}
