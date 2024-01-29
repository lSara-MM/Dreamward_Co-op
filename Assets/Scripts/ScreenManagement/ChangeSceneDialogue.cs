using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneDialogue : MonoBehaviour
{
    [SerializeField] private bool pressAnyKey = false;
    [SerializeField] private string passToScene;
    [SerializeField] private FloatSO dialogue;
    [SerializeField] private FloatSO boss1;
    [SerializeField] private FloatSO boss2;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (pressAnyKey && Input.anyKeyDown && !Input.GetKey(KeyCode.Escape) && !(Input.GetMouseButtonDown(0) 
            || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
        {
            ChangeToScene(passToScene);
        }
    }

    public void ChangeToScene(string passToScene)
    {
        Debug.Log("Change Scene" + passToScene);
        SceneManager.LoadScene(passToScene);
    }
}
