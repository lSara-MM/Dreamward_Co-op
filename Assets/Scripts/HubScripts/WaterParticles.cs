using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticles : MonoBehaviour
{
    public GameObject water;
    public GameObject player;
    public AudioSource splashSound;

    public bool splash = false;

    // Update is called once per frame
    void Update()
    {
       //Vector3 pos = new Vector3(player.transform.position.x, water.transform.position.y, water.transform.position.z);
       //water.transform.position = pos;

        if (!splash)
        {
            water.SetActive(false);
        }

        if (splash) 
        {
            water.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        splashSound.Play();
        splash = true;
    }
}
