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

    // Start is called before the first frame update
    void Start()
    {
        //dialogue.Value = dialogueScene;
    }

    // Update is called once per frame
    void Update()
    {
        if (pressAnyKey == PressKey.ANY_KEY && (Input.GetButtonUp("Jump") || Input.anyKeyDown) && !Input.GetKey(KeyCode.Escape))
        {
            if (passToScene != "")
            {
                ChangeToScene(passToScene);
            }
        }
        else if (pressAnyKey == PressKey.ON_CLICK && (Input.GetButtonUp("Jump") ||
            (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))) && !Input.GetKey(KeyCode.Escape))
        {
            if (passToScene != "")
            {
                ChangeToScene(passToScene);
            }
        }
    }

    public void ChangeToScene(string passToScene)
    {
        Debug.Log("Change Scene" + passToScene);
        SceneManager.LoadScene(passToScene);
    }
}
