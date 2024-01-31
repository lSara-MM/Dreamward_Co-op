using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private bool pressAnyKey = false;
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
        if (pressAnyKey && (Input.GetButtonUp("Jump") || Input.anyKeyDown) && !Input.GetKey(KeyCode.Escape))
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
