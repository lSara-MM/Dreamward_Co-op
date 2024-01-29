using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SecondPhaseHub : MonoBehaviour
{
    public ChangeScene changeScene;

    public Light2D globalLight;
    public Light2D lampLight;
    public Light2D bellLamp1;
    public Light2D bellLamp2;
    public Light2D playerLight;

    public GameObject Ground;
    public GameObject canvas;
    public BoxCollider2D coll;
    public SpriteRenderer fadeToBlack;

    public Rigidbody2D rbPlayer;

    public GameObject Bell1;
    public GameObject Bell2;

    public SpriteRenderer waterTop;
    public SpriteRenderer waterSide;
    public SpriteRenderer groundSprite;

    public SpriteRenderer player;
    public SpriteRenderer scissors;

    public Text text1;
    public Text text2;
    public Text text3;

    public string scene;

    public CinemachineVirtualCamera virtualCamera;

    public bool secondPhase = false;
    float timing = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.LeftShift)) 
        {
            secondPhase = true;
            scene = "Boss 1 Clown";
        }
        
        if (Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.LeftShift)) 
        {
            secondPhase = true;
            scene = "Boss 2 PerroSanchez";
        }

        if (!secondPhase) 
        {
            globalLight.intensity = 1.32f;
            lampLight.intensity = 3.76f;
            bellLamp1.intensity = 3.12f;
            bellLamp2.intensity = 3.12f;
            playerLight.intensity = 3.51f;

            globalLight.color = new Color(0.101f, 0.249f, 0.377f, 1f);
            waterTop.color = new Color(1f, 1f, 1f, 0.607f);
            waterSide.color = new Color(0.094f, 0.289f, 0.339f, 1f);
            groundSprite.color = new Color(0.403f, 0.660f, 0.830f, 0.580f);
            player.color = new Color(1f, 1f, 1f, 1f);
            scissors.color = new Color(1f, 0.853f, 0f, 1f);
            //text1.color = new Color(0f, 0.636f, 1f, 1f);
            //text2.color = new Color(0f, 0.636f, 1f, 1f);

            coll = Ground.GetComponent<BoxCollider2D>();
            canvas.SetActive(true);

            virtualCamera.m_Lens.OrthographicSize = 4.2f;
            timing = 0f;
        }
        else 
        {
            timing += Time.deltaTime;

            coll.isTrigger = true;
            canvas.SetActive(false);

            if (fadeToBlack.color.a < 1f)
            {
                fadeToBlack.color = new Color(0f, 0f, 0f, fadeToBlack.color.a + 0.5f * Time.deltaTime);
            }

            if (globalLight.intensity > 0f) 
            {
                globalLight.intensity -= 0.005f * timing;
            }
            
            if (lampLight.intensity > 0f) 
            {
                lampLight.intensity -= 0.005f * timing;
            }
            
            if (bellLamp1.intensity > 0f) 
            {
                bellLamp1.intensity -= 0.005f * timing;
            }
            
            if (bellLamp2.intensity > 0f) 
            {
                bellLamp2.intensity -= 0.005f * timing;
            }

            if (playerLight.intensity > 0f)
            {
                playerLight.intensity -= 0.005f * timing;
            }

            if (rbPlayer.gravityScale > 0.2f) 
            {
                rbPlayer.gravityScale -= 0.05f * timing;
            }
            
            if (virtualCamera.m_Lens.OrthographicSize > 2.2f) 
            {
                virtualCamera.m_Lens.OrthographicSize -= 0.005f * timing;
            }

            if(waterTop.color.a > 0) 
            {
                waterTop.color = new Color(waterTop.color.r, waterTop.color.g, waterTop.color.b, waterTop.color.a - 0.005f * timing);
            }
            
            if(waterSide.color.a > 0) 
            {
                waterSide.color = new Color(waterSide.color.r, waterSide.color.g, waterSide.color.b, waterSide.color.a - 0.005f * timing);
            }
            
            if(groundSprite.color.a > 0) 
            {
                groundSprite.color = new Color(groundSprite.color.r, groundSprite.color.g, groundSprite.color.b, groundSprite.color.a - 0.005f * timing);
            }
            
            if(text1.color.a > 0) 
            {
                text1.color = new Color(text1.color.r, text1.color.g, text1.color.b, text1.color.a - 0.005f * timing);
            } 
            
            if(text2.color.a > 0) 
            {
                text2.color = new Color(text2.color.r, text2.color.g, text2.color.b, text2.color.a - 0.005f * timing);
            }
            
            if(text3.color.a > 0) 
            {
                text3.color = new Color(text3.color.r, text3.color.g, text3.color.b, text3.color.a - 0.005f * timing);
            }
            
            if(player.color.a > 0) 
            {
                player.color = new Color(player.color.r, player.color.g, player.color.b, player.color.a - 0.005f * timing);
            }
            
            if(scissors.color.a > 0) 
            {
                scissors.color = new Color(scissors.color.r, scissors.color.g, scissors.color.b, scissors.color.a - 0.005f * timing);
            }

            if (timing > 3f)
            {
                globalLight.intensity = 1f;
                globalLight.color = new Color(0, 0, 0, 1); 
                timing = 0f;

                changeScene.ChangeToScene(scene);
            }
        }
    }
}
