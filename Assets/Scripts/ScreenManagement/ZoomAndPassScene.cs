using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomAndPassScene : MonoBehaviour
{
    public ChangeScene changeScene;
    public SimpleFadeToBlack fade;

    public GameObject music;

    float timing = 0f;

    public bool startTransition = false;

    private void Awake()
    {
        DontDestroyOnLoad(music);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTransition = true;
        }

        if (startTransition)
        {
            if (fade.Fade())
            {
                changeScene.ChangeToScene("LoreMaster");
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
    }
}
