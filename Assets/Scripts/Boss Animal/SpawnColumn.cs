using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnColumn : MonoBehaviour
{

    [SerializeField] GameObject pilar;
    [SerializeField] float killHeigth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool pilarCreated = false;
    float dtPilar = 0.0f;
    GameObject instancePilar = null;

    // Update is called once per frame
    void Update()
    {
        if (pilar != null) 
        {
            if(true && !pilarCreated) //Recibir un mensaje
            {
                instancePilar = Instantiate(pilar);
                instancePilar.transform.position = transform.transform.position;
                pilarCreated = true;
                dtPilar = 0.0f;
            }
            if(pilarCreated) 
            {
                dtPilar += Time.deltaTime;
                instancePilar.transform.transform.position = instancePilar.transform.transform.position + new Vector3(0,0.1f, 0);
                if(instancePilar.transform.transform.position.y > killHeigth) 
                {
                    Destroy(instancePilar);
                    instancePilar = null;
                    pilarCreated = false;
                }
            }
        }
    }
}
