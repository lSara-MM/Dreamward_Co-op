using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpawnManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject player;
    public GameObject Particles;

    public Light2D ligthSpot;

    float timing = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Particles.SetActive(true);
        rb.gravityScale = 1f;
        player.transform.position = new Vector3(0f, 40f, 0f);
        ligthSpot.intensity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timing += Time.deltaTime;

        if(rb.velocity.y == 0f) 
        {
            Particles.SetActive(false);
            rb.gravityScale = 4f;
            ligthSpot.intensity = 3.47f;
            timing = 0f;
        }

        if(ligthSpot.intensity > 0f) 
        {
            if(timing > 0.1f && timing < 0.15f) 
            {
                ligthSpot.intensity = 0f;
            }
            
            if(timing > 0.15f && timing < 0.17f) 
            {
                ligthSpot.intensity = 2f;
            }

            if (timing > 0.17f && timing < 0.2f)
            {
                ligthSpot.intensity = 0f;
            }

            if (timing > 0.2f)
            {
                ligthSpot.intensity = 3.47f;
            }
        }
    }
}
