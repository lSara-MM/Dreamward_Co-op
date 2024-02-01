using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartParticles : MonoBehaviour
{
    [SerializeField] private bool _start = true;

    // Start is called before the first frame update
    void Start()
    {
        if (_start)
        {
            this.gameObject.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            this.gameObject.GetComponent<ParticleSystem>().Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
