using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomAndPassScene : MonoBehaviour
{
    public ChangeScene changeScene;
    public SimpleFadeToBlack fade;

    public GameObject[] images;

    public GameObject lines;

    public GameObject music;

    public float timing = 0f;

    int imgPasser = 0;

    public bool startTransition = false;

    public bool Intro = false;
    public bool Hub = false;
    public bool Credits = false;
    bool hasFaded = false;

    private void Awake()
    {
        DontDestroyOnLoad(music);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!Hub)
        {
            lines.SetActive(false);
        }
        else
        {
            lines.SetActive(true);
        }

        for (int i = 0; i < images.Length; i++)
        {
            images[i].SetActive(false);
        }

        if(images.Length > 0f)
        {
            images[0].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Credits)
        {
            timing += Time.deltaTime;
        }

        if (!startTransition && !hasFaded)
        {
            if (Input.GetMouseButtonDown(0) || Input.anyKeyDown || timing > 5f || Input.GetButtonDown("Submit"))
            {
                imgPasser++;
                startTransition = true;
                timing = 0f;
            }
        }

        if (startTransition && !fade.startBlackAndFade)
        {
            if (fade.Fade())
            {
                hasFaded = true;

                if (imgPasser == 1)
                {
                    lines.SetActive(true);
                }

                if (imgPasser < images.Length) 
                {
                    images[imgPasser - 1].SetActive(false);
                    images[imgPasser].SetActive(true);
                }

                if(imgPasser == images.Length)
                {
                    if (Intro)
                    {
                        changeScene.ChangeToScene("LoreMaster");
                    }

                    if (Hub)
                    {
                        changeScene.ChangeToScene("Hub");
                    }
                }

                startTransition = false;
            }

            //    timing += Time.deltaTime;

            //    if (fade.fadeToBlackImage.transform.localScale.x < 30f)
            //    {
            //        Debug.Log("Enter");
            //        fade.fadeToBlackImage.transform.localScale = new Vector3(fade.fadeToBlackImage.transform.localScale.x - 0.005f * timing, fade.fadeToBlackImage.transform.localScale.y - 0.005f * timing, fade.fadeToBlackImage.transform.localScale.z - 0.005f * timing);
            //    }
            //    else
            //    {
            //        Debug.Log(fade.fadeToBlackImage.transform.localScale.x);

            //        if (fade.Fade())
            //        {
            //            changeScene.ChangeToScene("LoreMaster");
            //        }

            //    }
        }

        if (hasFaded)
        {
            if (fade.unFade())
            {
                hasFaded = false;
                timing = 0f;
            }
        }
    }
}
