using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnColumn : MonoBehaviour
{
    [SerializeField] private GameObject pillar;
    [SerializeField] private ParticleSystem groundTremors;
    //[SerializeField] private ParticleSystem ghost;
    [SerializeField] private float killHeight = 13.0f;

    public AudioSource sound;
    public bool spawn;

    [Header("Bools guarros")]
    bool pillarCreated = false;
    GameObject instancePillar = null;
    float dtWait = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pillar != null) 
        {
            if(spawn && !pillarCreated) //Recibir un mensaje
            {
                instancePillar = Instantiate(pillar);
                instancePillar.transform.position = transform.transform.position;
                pillarCreated = true;
                groundTremors.Play();
                sound.Play();
                dtWait = 0.0f;
                //ghost.Stop();
            }
            if (pillarCreated) 
            {
                dtWait += Time.deltaTime;

                if (dtWait > groundTremors.main.duration)
                {
                    sound.Stop();
                    //if (dtWait > groundTremors.main.duration + 1.2f) {ghost.Play(); }
                    //if (!ghost.isPlaying) { ghost.Play(); }
                    instancePillar.transform.transform.position = instancePillar.transform.transform.position + new Vector3(0, 0.08f, 0);
                    if (instancePillar.transform.transform.position.y > killHeight)
                    {
                        Destroy(instancePillar);
                        //ghost.Stop();
                        instancePillar = null;
                        pillarCreated = false;
                        spawn = false;
                    }
                }
            }
            
        }
    }
}
