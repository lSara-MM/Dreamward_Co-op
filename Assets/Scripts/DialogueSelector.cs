using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSelector : MonoBehaviour
{
    public FloatSO Dialogue;
    public FloatSO Boss1;
    public FloatSO Boss2;
    public FloatSO Boss3;

    public GameObject IntroDialogue;
    public GameObject Boss1Dialogue;
    public GameObject Boss2Dialogue;
    public GameObject Boss3Dialogue;

    public ChangeSceneDialogue changeScene;

    public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("DialogueSound")) 
        {
            music.Play();
        }

        if (Dialogue.Value == 0) 
        {
            IntroDialogue.SetActive(true);
            Boss1Dialogue.SetActive(false);
            Boss2Dialogue.SetActive(false);
            Boss3Dialogue.SetActive(false);
        }
        
        if (Dialogue.Value == 1) 
        {
            if (Boss1.Value == 0)
            {
                IntroDialogue.SetActive(false);
                Boss1Dialogue.SetActive(true);
                Boss2Dialogue.SetActive(false);
                Boss3Dialogue.SetActive(false);
            }
            else 
            {
                changeScene.ChangeToScene("Boss 1 Clown");
            }
        }
        
        if (Dialogue.Value == 2) 
        {
            if (Boss2.Value == 0)
            {
                IntroDialogue.SetActive(false);
                Boss1Dialogue.SetActive(false);
                Boss3Dialogue.SetActive(false);
                Boss2Dialogue.SetActive(true);
            }
            else
            {
                changeScene.ChangeToScene("Boss 2 PerroSanchez");
            }
        }

        if (Dialogue.Value == 4)
        {
            if (Boss3.Value == 0)
            {
                IntroDialogue.SetActive(false);
                Boss1Dialogue.SetActive(false);
                Boss2Dialogue.SetActive(false);
                Boss3Dialogue.SetActive(true);
            }
            else
            {
                changeScene.ChangeToScene("Boss 3 Storm");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3") || Input.GetKeyDown(KeyCode.Space))// button jump = space/B button
        {
            if (Dialogue.Value == 0)
            {
                changeScene.ChangeToScene("Tutorial");
            }

            if (Dialogue.Value == 1)
            {
                changeScene.ChangeToScene("Boss 1 Clown");
            }

            if (Dialogue.Value == 2)
            {
                changeScene.ChangeToScene("Boss 2 PerroSanchez");
            }
            
            if (Dialogue.Value == 4)
            {
                changeScene.ChangeToScene("Boss 3 Storm");
            }
        }
    }
}
