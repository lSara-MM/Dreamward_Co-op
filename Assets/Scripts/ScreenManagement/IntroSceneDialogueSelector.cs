using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneDialogueSelector : MonoBehaviour
{
    public FloatSO dialogue;
    public int dialogueSelector;

    // Start is called before the first frame update
    void Start()
    {
        dialogue.Value = dialogueSelector;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
