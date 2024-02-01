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
    public GameObject dificultyGM;
    private DificultySelector dificultySelector;

    public AudioSource bellSound;

    [SerializeField] private LevelsManager _lvlsManager;

    // Start is called before the first frame update
    void Start()
    {
        dificultyGM = GameObject.Find("DifultySelector");
        dificultySelector = dificultyGM.GetComponent<DificultySelector>();

        isRaining = false;
        dificultySelector.hardMode = isRaining;
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
                rainParticle1.Play();
                rainParticle2.Play();
            }
            else
            {
                rainParticle1.Pause();
                rainParticle2.Pause();
            }

            dificultySelector.hardMode = isRaining;
            rain.SetActive(isRaining);

            // Change bells' colors
            _lvlsManager.ChangeNightmare(isRaining);
        }
    }
}
