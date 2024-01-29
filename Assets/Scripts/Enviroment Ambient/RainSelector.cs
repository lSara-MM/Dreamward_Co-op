using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RainSelector : MonoBehaviour
{
    public bool isRaining;
    public ParticleSystem rainParticle1;
    public ParticleSystem rainParticle2;

    public GameObject rain;
    public DificultySelector dificultySelector;

    public AudioSource bellSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bellSound.Play();
            isRaining = !isRaining;

            if (isRaining)
            {
                dificultySelector.hardMode = true;
                rain.SetActive(true);
                rainParticle1.Play();
                rainParticle2.Play();
            }
            else
            {
                dificultySelector.hardMode = false;
                rain.SetActive(false);
                rainParticle1.Pause();
                rainParticle2.Pause();
            }
        }
    }
}
