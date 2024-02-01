using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CloudBlink : MonoBehaviour
{
    public ParticleSystem particula;
    public Color colorToChange;
    [SerializeField] private BossHealth vidaBoss;
    private Color originalParticleColor;
    float timeDT = 0.0f;
    bool startCount = false;

    // Start is called before the first frame update
    void Start()
    {
        originalParticleColor = particula.startColor; //Servira esto? Quiza tengo que guardar algo mas complejo
    }

    // Update is called once per frame
    void Update()
    {
        if (startCount) { timeDT += Time.deltaTime; }
        
        if (vidaBoss._hitBoss) 
        {
            particula.startColor = colorToChange;
            startCount = true; //Timer de que se quede printado
        }
        else if(timeDT>0.5f)
        {
            particula.startColor = originalParticleColor;
            timeDT = 0.0f;
            startCount = false;
        }
    }
}
