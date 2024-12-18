using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PassTutorial : MonoBehaviour
{
    public ChangeScene changeScene;
    public SpriteRenderer fadeToBlack;

    public GameObject canvas;

    BoxCollider2D coll;
    public GameObject Ground;

    public string scene;
    public bool secondPhase = false;
    float timing = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.LeftShift))
        {
            secondPhase = true;
        }

        if (!secondPhase)
        {
            //fadeToBlack.color = new Color(0f, 0f, 0f, 0f);
            canvas.SetActive(true);
            coll = Ground.GetComponent<BoxCollider2D>();

            timing = 0f;
        }
        else
        {
            timing += Time.deltaTime;
            canvas.SetActive(false);
            coll.isTrigger = true;

            if (fadeToBlack.color.a < 1f)
            {
                fadeToBlack.color = new Color(0f, 0f, 0f, fadeToBlack.color.a + 0.5f * Time.deltaTime);
            }

            if (timing > 2.8f && fadeToBlack.color.a >= 1f)
            {
                fadeToBlack.color = new UnityEngine.Color(0f, 0f, 0f, 1f);
                timing = 0f;

                changeScene.ChangeToScene(scene);
            }
        }
    }
}
