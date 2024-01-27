using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    private float time = 0.0f;
    [SerializeField] private Animator fade;
    [SerializeField] private GameObject black;

    // Start is called before the first frame update
    void Start()
    {
        fade = black.GetComponent<Animator>();
        fade.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > 2.0f)
        {
            fade.enabled = true;
        }
        if (time > 2.95f)
        {
             SceneManager.LoadScene("IntroScene");
        }
    }
}
