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

    public SpriteRenderer fadeToBlack;

    public float timing = 0f;

    public bool Tickling = false;
    public bool timerReset = false;

    // Start is called before the first frame update
    void Start()
    {
        Particles.SetActive(true);
        rb.gravityScale = 1f;
        player.transform.position = new Vector3(0f, 40f, 0f);
        ligthSpot.intensity = 0f;

        fadeToBlack.color = new Color(0f, 0f, 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        timing += Time.deltaTime;

        if (fadeToBlack.color.a > 0f) 
        {
            fadeToBlack.color = new Color(0f, 0f, 0f, fadeToBlack.color.a - 0.5f * Time.deltaTime);
        }

        if (!Tickling)
        {
            if (!timerReset)
            {
                if (rb.velocity.y == 0f)
                {
                    Particles.SetActive(false);
                    rb.gravityScale = 4f;
                    ligthSpot.intensity = 3.47f;
                    timing = 0f;
                    timerReset = true;
                }
            }

            if (ligthSpot.intensity > 0f)
            {
                if (timing > 0f && timing < 0.2f)
                {
                    ligthSpot.intensity = 0.1f;
                }

                if (timing > 0.2f && timing < 0.5f)
                {
                    ligthSpot.intensity = 2f;
                }

                if (timing > 0.5f && timing < 0.7f)
                {
                    ligthSpot.intensity = 0.1f;
                } 
                
                if (timing > 0.7f && timing < 0.8f)
                {
                    ligthSpot.intensity = 3f;
                }
                
                if (timing > 0.8f && timing < 1f)
                {
                    ligthSpot.intensity = 0.1f;
                }

                if (timing > 1f)
                {
                    ligthSpot.intensity = 3.47f;
                    Tickling = true;
                }
            }
        }
    }
}
