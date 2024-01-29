using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fadeToBlack;
    [SerializeField] private float fadeSpeed = 0.5f;
    [SerializeField] private float fadeMax = 1f;

    private float _timing = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _timing = 0;
    }

    public bool Fade()
    {
        _timing += Time.deltaTime;

        if (fadeToBlack.color.a < fadeMax)
        {
            fadeToBlack.color = new Color(0f, 0f, 0f, fadeToBlack.color.a + fadeSpeed * Time.deltaTime);
            return false;
        }
        else
        {
            fadeToBlack.color = new Color(0f, 0f, 0f, fadeMax);
            _timing = 0;
            return true;
        }
    }
}
