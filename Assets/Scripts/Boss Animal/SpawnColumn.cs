using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnColumn : MonoBehaviour
{

    [SerializeField] GameObject pilar;
    [SerializeField] ParticleSystem groundTremors;
    [SerializeField] float killHeigth = 13.0f;
    public bool spawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool pilarCreated = false;
    GameObject instancePilar = null;
    float dtWait = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (pilar != null) 
        {
            if(spawn && !pilarCreated) //Recibir un mensaje
            {
                instancePilar = Instantiate(pilar);
                instancePilar.transform.position = transform.transform.position;
                pilarCreated = true;
                groundTremors.Play();
                dtWait = 0.0f;
            }
            if (pilarCreated) 
            {
                dtWait += Time.deltaTime;

                if (dtWait > groundTremors.main.duration)
                {
                    instancePilar.transform.transform.position = instancePilar.transform.transform.position + new Vector3(0, 0.1f, 0);
                    if (instancePilar.transform.transform.position.y > killHeigth)
                    {
                        Destroy(instancePilar);
                        instancePilar = null;
                        pilarCreated = false;
                        spawn = false;
                    }
                }
            }
            
        }
    }
}
