using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum PressKey
{
    NONE,
    ANY_KEY,
    ON_CLICK,
}

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private PressKey pressAnyKey = PressKey.NONE;
    [SerializeField] private string passToScene;

    [Header("Serialized vars")]
    [SerializeField] private FloatSO dialogue;
    [SerializeField] private FloatSO boss1;
    [SerializeField] private FloatSO boss2;
    //[SerializeField] public int dialogueScene; // 0 = Intro dialogue, 1 = Boss 1 dialogue, 2 = Boss 2 Dialogue, 3 = None

    // Variables para FadeToBlack
    public SimpleFadeToBlack fadeToBlack;
    public bool IsIntroScene = false;
    bool IsFading = false;

    public List<GameObject> dontDestroyTempList;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in dontDestroyTempList)
        {
            Globals.dontDestroyList.Add(go);
        }

        //if (passBlack && fadeToBlack != null) 
        //{
        //    fadeToBlack.fadeToBlackImage.color = new Color(1f, 1f, 1f, 1f);
        //}

        //dialogue.Value = dialogueScene;
    }

    // Update is called once per frame
    void Update()
    {
        if (pressAnyKey == PressKey.ANY_KEY && (Input.GetButtonUp("Jump") || Input.anyKeyDown) && !Input.GetKey(KeyCode.Escape))
        {
            if (fadeToBlack != null)
            {
                if (passToScene != "" && !fadeToBlack.startBlackAndFade)
                {
                    if (IsIntroScene)
                    {
                        IsFading = true;
                    }
                    else
                    {
                        ChangeToScene(passToScene);
                    }
                }
            }
            else
            {
                ChangeToScene(passToScene);
            }
        }
        else if (pressAnyKey == PressKey.ON_CLICK && (Input.GetButtonUp("Jump") ||
            (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))) && !Input.GetKey(KeyCode.Escape))
        {
            if (fadeToBlack != null)
            {
                if (passToScene != "" && !fadeToBlack.startBlackAndFade)
                {
                    if (IsIntroScene)
                    {
                        IsFading = true;
                    }
                    else
                    {
                        ChangeToScene(passToScene);
                    }
                }
            }
            else
            {
                ChangeToScene(passToScene);
            }
        }

        if (IsFading)
        {
            if (fadeToBlack != null)
            {
                if (fadeToBlack.Fade())
                {
                    ChangeToScene(passToScene);

                    IsIntroScene = false;
                }
            }
        }
    }

    public void ChangeToScene(string passToScene)
    {
        Debug.Log("Change Scene " + passToScene);

        foreach (GameObject item in Globals.dontDestroyList)
        {
            DontDestroyOnLoad(item);
        }

        Serialization cs_Serialization = GameObject.FindGameObjectWithTag("Serialization").GetComponent<Serialization>();
        cs_Serialization.SerializeData(default, ACTION_TYPE.CHANGE_SCENE, passToScene);

        SceneManager.LoadScene(passToScene);
    }
}
