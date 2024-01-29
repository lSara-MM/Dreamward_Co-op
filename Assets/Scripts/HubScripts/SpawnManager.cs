using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpawnManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject player;
    public GameObject Particles;
    public GameObject Rain;

    public AudioSource fallSound;
    public AudioSource splashSound;
    public AudioSource flickeringLampSound;
    public AudioSource waterSound;
    public AudioSource ambientMusic;

    public Light2D ligthSpot;

    public SpriteRenderer fadeToBlack;

    PlayerMovement playerMovement;

    public float timing = 0f;

    public bool Tickling = false;
    public bool timerReset = false;
    public bool isHub;
    public bool isRaining;

    public RainSelector rainSelector;

    // Start is called before the first frame update
    void Start()
    {
        Particles.SetActive(true);
        rb.gravityScale = 1f;
        player.transform.position = new Vector3(0f, 40f, 0f);
        ligthSpot.intensity = 0f;
        fallSound.volume = 0f; 
        ambientMusic.volume = 0f;

        fadeToBlack.color = new Color(0f, 0f, 0f, 1f);

        playerMovement = player.GetComponent<PlayerMovement>();

        Tickling = false;
        timerReset = false;

        if (isHub) 
        {
            int isRaining = Random.Range(0, 2);

            if(isRaining == 1) 
            {
                Rain.SetActive(true);
                rainSelector.isRaining = true;
            }
            else 
            {
                Rain.SetActive(false);
                rainSelector.isRaining = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timing += Time.deltaTime;

        if (!Tickling)
        {
            if (fadeToBlack.color.a > 0f)
            {
                fadeToBlack.color = new Color(0f, 0f, 0f, fadeToBlack.color.a - 0.5f * Time.deltaTime);
            }

            if (!timerReset)
            {
                if (fallSound.volume < 1f)
                {
                    fallSound.volume += 0.3f * Time.deltaTime;
                }

                if (ambientMusic.volume < 0.262f)
                {
                    ambientMusic.volume += 0.05f * Time.deltaTime;
                }

                if (rb.velocity.y == 0f && fadeToBlack.color.a <= 0f && playerMovement.IsGrounded())
                {
                    Particles.SetActive(false);
                    rb.gravityScale = 4f;
                    ligthSpot.intensity = 3.47f;
                    timing = 0f;
                    timerReset = true;
                    splashSound.Play();
                    flickeringLampSound.Play();
                    waterSound.Play();
                }
            }

            if (ligthSpot.intensity > 0f)
            {
                if (fallSound.volume > 0f)
                {
                    fallSound.volume -= 0.8f * Time.deltaTime;
                }

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
