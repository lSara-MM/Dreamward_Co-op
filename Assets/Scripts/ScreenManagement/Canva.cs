using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canva : MonoBehaviour
{
    [SerializeField] private UIFadeFromBlack fade;
    [SerializeField] private bool hasFaded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasFaded)
        {
            hasFaded = fade.UnFadeUI();
        }
    }
}
