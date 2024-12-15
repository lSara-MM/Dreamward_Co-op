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
    public Light2D moon;

    public GameObject lamp;
    public GameObject AllEyes;
    public GameObject SpotLight1;
    public GameObject SpotLight2;
    public GameObject Curtain;
    public GameObject Curtain1;

    [SerializeField] private BossHealth bossHealth;

    float timing = 0f;

    void Start()
    {
        bossHealth = GameObject.Find("Enemy").GetComponent<BossHealth>();
        bossEye1 = GameObject.Find("Light 2D Boss1").GetComponent<Light2D>();
        bossEye2 = GameObject.Find("Light 2D Boss2").GetComponent<Light2D>();
        bossEye3 = GameObject.Find("Light 2D Boss3").GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.LeftShift))
        {
            bossHealth.bossSP = true;
        }

        if (!bossHealth.bossSP) 
        {
            globalLight.intensity = 1.32f;
            globalLight.color = new Color(0.580f, 0.654f, 0.764f, 1f);

            lamp.SetActive(false);
            AllEyes.SetActive(false);
            SpotLight1.SetActive(true);
            SpotLight2.SetActive(true);
            Curtain.SetActive(true);
            Curtain1.SetActive(true);

            bossEye1.intensity = 0.57f;
            bossEye2.intensity = 0.57f;
            bossEye3.intensity = 0.57f;

            plattform1.intensity = 0f;
            plattform2.intensity = 0f;

            moon.intensity = 0.59f;
            timing = 0f;
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
        AllEyes.SetActive(true);
        SpotLight1.SetActive(false);
        SpotLight2.SetActive(false);
        Curtain.SetActive(false);
        Curtain1.SetActive(false);

        bossEye1.intensity = 1.86f;
        bossEye2.intensity = 1.86f;
        bossEye3.intensity = 0.63f;

        plattform1.intensity = 1.15f;
        plattform2.intensity = 1.15f;

        moon.intensity = 2.48f;
    }
}
