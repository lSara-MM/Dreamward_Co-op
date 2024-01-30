using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleFadeToBlack : MonoBehaviour
{
    public Image fadeToBlackImage;
    public bool fadeToBlack = true;
    public bool startBlackAndFade = true;
    public bool auto = false;

    // Start is called before the first frame update
    void Start()
    {
        if (startBlackAndFade) 
        {
            fadeToBlackImage.color = new Color(fadeToBlackImage.color.r, fadeToBlackImage.color.g, fadeToBlackImage.color.b, 1f);
        }
        else
        {
            fadeToBlackImage.color = new Color(fadeToBlackImage.color.r, fadeToBlackImage.color.g, fadeToBlackImage.color.b, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeToBlackImage != null)
        {
            if (startBlackAndFade)
            {
                if (unFade())
                {
                    startBlackAndFade = false;
                }
            }

            if (auto)
            {
                if (fadeToBlack)
                {
                    if (Fade())
                    {
                        fadeToBlack = false;
                    }
                }

                if (!fadeToBlack)
                {
                    if (fadeToBlackImage.color.a > 0f)
                    {
                        unFade();
                    }
                }
            }
        }
    }

    public bool Fade()
    {
        bool hasEnded = false;

        if (fadeToBlackImage != null)
        {
            if (fadeToBlackImage.color.a < 1f)
            {
                fadeToBlackImage.color = new Color(fadeToBlackImage.color.r, fadeToBlackImage.color.g, fadeToBlackImage.color.b, fadeToBlackImage.color.a + 1.2f * Time.deltaTime);
            }
            else
            {
                hasEnded = true;
            }
        }

        return hasEnded;
    }

    public bool unFade()
    {
        bool hasEnded = false;

        if (fadeToBlackImage != null)
        {
            if (fadeToBlackImage.color.a > 0f)
            {
                fadeToBlackImage.color = new Color(fadeToBlackImage.color.r, fadeToBlackImage.color.g, fadeToBlackImage.color.b, fadeToBlackImage.color.a - 1.2f * Time.deltaTime);
            }
            else
            {
                hasEnded = true;
            }
        }

        return hasEnded;
    }
}
