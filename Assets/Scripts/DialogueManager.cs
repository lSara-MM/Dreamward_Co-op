using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject[] dialogue;
    public int storyTeller = 0;
    public UIFadeToBlack fadeToBlack;
    public UIFadeFromBlack fadeFromBlack;

    public bool textHasChanged;
    public bool returnFromBlack;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < dialogue.Length; i++)
        {
            dialogue[i].SetActive(false);
        }

        textHasChanged = false;
        returnFromBlack = false;

        dialogue[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && storyTeller < dialogue.Length - 1)
        {
            textHasChanged = true;
        }

        if (textHasChanged)
        {
            Debug.Log("Is fading");

            if (!fadeToBlack.FadeUI())
            {
                dialogue[storyTeller].SetActive(false);

                storyTeller++;

                dialogue[storyTeller].SetActive(true);

                textHasChanged = false;

                Debug.Log("Black");
            }
        }

        if (returnFromBlack)
        {
            Debug.Log("Return black");

            if (!fadeFromBlack.UnFadeUI()) 
            {
                returnFromBlack = false;

                Debug.Log("Normal");
            }
        }
    }
}
