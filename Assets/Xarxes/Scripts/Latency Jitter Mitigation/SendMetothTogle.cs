using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMetothTogle : MonoBehaviour
{

    static public bool sendWithJitter = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //sendWithJitter = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            sendWithJitter = !sendWithJitter;
            Debug.Log("sendWithJitter ahora es: " + sendWithJitter);
        }
    }
}
