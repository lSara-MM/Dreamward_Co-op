using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingAmbient : MonoBehaviour
{
    public GameObject[] lightings;
    int actualLigth = 0;
    float timing = 0f;
    float coolDown = 0f;

    bool waitLight = false;

    float minTime = 0f; // Tiempo minimo que tarda un rayo en aparecer
    float maxTime = 5f; // Tiempo máximo que tarda un rayo en aparecer
    float appearenceTime = 0.3f; //Tiempo que un rayo se mantiene encendido

    // Start is called before the first frame update
    void Start()
    {
        actualLigth = Random.Range(0, lightings.Length);

        for(int i = 0; i < lightings.Length; i++)
        {
            lightings[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timing += Time.deltaTime;

        if (!waitLight)
        {
            if (timing >= Random.Range(minTime, maxTime))
            {
                actualLigth = Random.Range(0, lightings.Length);
                waitLight = true;
            }
        }

        if (waitLight) 
        { 
            coolDown += Time.deltaTime;

            lightings[actualLigth].SetActive(true);

            if (coolDown >= appearenceTime)
            {
                lightings[actualLigth].SetActive(false);
                coolDown = 0f;
                timing = 0f;
                waitLight = false;
            }
        }
    }
}
