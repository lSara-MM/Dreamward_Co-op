using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSelector : MonoBehaviour
{
    public FloatSO Dialogue;
    public FloatSO Boss1;
    public FloatSO Boss2;

    public GameObject IntroDialogue;
    public GameObject Boss1Dialogue;
    public GameObject Boss2Dialogue;

    public ChangeSceneDialogue changeScene;

    // Start is called before the first frame update
    void Start()
    {
        if (Dialogue.Value == 0) 
        {
            IntroDialogue.SetActive(true);
            Boss1Dialogue.SetActive(false);
            Boss2Dialogue.SetActive(false);
        }
        
        if (Dialogue.Value == 1) 
        {
            if (Boss1.Value == 0)
            {
                IntroDialogue.SetActive(false);
                Boss1Dialogue.SetActive(true);
                Boss2Dialogue.SetActive(false);
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
                Boss2Dialogue.SetActive(true);
            }
            else
            {
                changeScene.ChangeToScene("Boss 2 PerroSanchez");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Andreu pon aquí el control para el mando para Skipear
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
        }
    }
}
