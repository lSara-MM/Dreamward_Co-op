using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelectorBell : MonoBehaviour
{
    public string PassScene;
    public SecondPhaseHub SceneManager;

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
        if (PassScene != null && collision.gameObject.tag == "Player")
        {
            bellSound.Play();
            SceneManager.scene = PassScene;
            SceneManager.secondPhase = true;
        }
    }
}
