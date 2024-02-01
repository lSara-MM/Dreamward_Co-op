using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public FloatSO Dialogue;
    public FloatSO Boss1;
    public FloatSO Boss2;
    public FloatSO Boss3;

    public GameObject[] dialogue;
    public GameObject[] draws;
    public int storyTeller = 0;
    public UIFadeToBlack fadeToBlack;
    public UIFadeFromBlack fadeFromBlack;

    public bool textHasChanged;
    public bool returnFromBlack;
    bool start = false;

    public Image fadeToBlackImage;

    public ChangeSceneDialogue changeScene;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < dialogue.Length; i++)
        {
            dialogue[i].SetActive(false);
        }

        textHasChanged = false;
        returnFromBlack = false;
        start = true;
        DrawsInOrder();

        dialogue[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (start) 
        {
            if (unFade())
            {
                start = false;
            }
        }

        if (!start)
        {
            if (Input.GetButtonUp("Submit") || ((Input.anyKeyDown) && !Input.GetKeyDown(KeyCode.Space)))
            {
                textHasChanged = true;
            }
        }

        if (storyTeller < dialogue.Length - 1)
        {
            if (textHasChanged && !returnFromBlack)
            {
                if (Fade())
                {
                    dialogue[storyTeller].SetActive(false);

                    storyTeller++;

                    DrawsInOrder();

                    if (storyTeller != dialogue.Length)
                    {
                        dialogue[storyTeller].SetActive(true);
                    }

                    textHasChanged = false;
                    returnFromBlack = true;
                }
            }
        }

        if (textHasChanged && !returnFromBlack && storyTeller >= dialogue.Length - 1)
        {
            if (Fade())
            {
                if (Dialogue.Value == 0)
                {
                    changeScene.ChangeToScene("Tutorial");
                }

                if (Dialogue.Value == 1)
                {
                    Boss1.Value = 1;
                    changeScene.ChangeToScene("Boss 1 Clown");
                }

                if (Dialogue.Value == 2)
                {
                    Boss2.Value = 1;
                    changeScene.ChangeToScene("Boss 2 PerroSanchez");
                }
                
                if (Dialogue.Value == 4)
                {
                    Boss3.Value = 1;
                    changeScene.ChangeToScene("Boss 3 Storm");
                }
            }
        }

        if (returnFromBlack)
        {
            if (unFade()) 
            {
                returnFromBlack = false;
            }
        }
    }

    private void DrawsInOrder()
    {
        if(Dialogue.Value == 0) 
        {
            if(storyTeller == 0) 
            {
                draws[0].SetActive(true);
            }

            if (storyTeller == 1)
            {
                draws[1].SetActive(true);
                draws[0].SetActive(false);
            }
            
            if (storyTeller == 2)
            {
                draws[1].SetActive(false);
                draws[2].SetActive(true);
            }

            if (storyTeller == 4)
            {
                draws[2].SetActive(false);
                draws[1].SetActive(true);
            }

            if (storyTeller == 5)
            {
                draws[1].SetActive(false);
                draws[3].SetActive(true);
            }
        }

        if (Dialogue.Value == 1)
        {
            if (storyTeller == 0)
            {
                draws[4].SetActive(true);
            }

            if (storyTeller == 1)
            {
                draws[4].SetActive(false);
                draws[1].SetActive(true);
            }

            if (storyTeller == 2)
            {
                draws[1].SetActive(false);
                draws[5].SetActive(true);
            }

            if (storyTeller == 3)
            {
                draws[5].SetActive(false);
                draws[6].SetActive(true);
            }
        }

        if (Dialogue.Value == 2)
        {
            if (storyTeller == 0)
            {
                draws[5].SetActive(true);
            }
            
            if (storyTeller == 1)
            {
                draws[5].SetActive(false);
                draws[6].SetActive(true);
            }

            if (storyTeller == 2)
            {
                draws[6].SetActive(false);
                draws[4].SetActive(true);
            }

            if (storyTeller == 3)
            {
                draws[4].SetActive(false);
                draws[1].SetActive(true);
            }
            
            if (storyTeller == 4)
            {
                draws[1].SetActive(false);
                draws[1].SetActive(true);
            }
        }

        if (Dialogue.Value == 4)
        {
            if (storyTeller == 0)
            {
                draws[5].SetActive(true);
            }

            if (storyTeller == 1)
            {
                draws[5].SetActive(false);
                draws[6].SetActive(true);
            }

            if (storyTeller == 2)
            {
                draws[6].SetActive(false);
                draws[4].SetActive(true);
            }

            if (storyTeller == 3)
            {
                draws[4].SetActive(false);
                draws[1].SetActive(true);
            }

            if (storyTeller == 4)
            {
                draws[1].SetActive(false);
                draws[1].SetActive(true);
            }
        }
    }

    private bool Fade() 
    {
        bool hasEnded = false;

        if (fadeToBlackImage.color.a < 1f) 
        {
            fadeToBlackImage.color = new Color(fadeToBlackImage.color.r, fadeToBlackImage.color.g, fadeToBlackImage.color.b, fadeToBlackImage.color.a + 1.2f * Time.deltaTime);
        }
        else 
        {
            hasEnded = true;
        }

        return hasEnded;
    }

    private bool unFade() 
    {
        bool hasEnded = false;

        if (fadeToBlackImage.color.a > 0f)
        {
            fadeToBlackImage.color = new Color(fadeToBlackImage.color.r, fadeToBlackImage.color.g, fadeToBlackImage.color.b, fadeToBlackImage.color.a - 1.2f * Time.deltaTime);
        }
        else
        {
            hasEnded = true;
        }

        return hasEnded;
    }
}
