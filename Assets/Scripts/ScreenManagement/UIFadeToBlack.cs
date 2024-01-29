using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIFadeToBlack : MonoBehaviour
{
    [SerializeField] private Image[] listFadeImg;
    [SerializeField] private Text[] listFadeText;

    [SerializeField] private float fadeSpeed = 0.5f;
    [SerializeField] private float fadeMax = 0f;

    private float _timing = 0f;
    private bool _hasFaded = false;

    private Color _aux;

    // Start is called before the first frame update
    void Start()
    {
        _timing = 0;
    }

    public bool FadeUI()
    {
        _timing += Time.deltaTime;

        for (int i = 0; i < listFadeImg.Length; i++)
        {
            if (listFadeImg[i].color.a > fadeMax)
            {
                _aux = listFadeImg[i].color;
                _aux.a = listFadeImg[i].color.a - fadeSpeed * Time.deltaTime;
                listFadeImg[i].color = _aux;

                _hasFaded = false;
            }
            else
            {
                _aux = listFadeImg[i].color;
                _aux.a = fadeMax;
                listFadeImg[i].color = _aux;

                _timing = 0;
                _hasFaded = true;
            }
        }

        for (int i = 0; i < listFadeText.Length; i++)
        {
            if (listFadeText[i].color.a > fadeMax)
            {
                _aux = listFadeText[i].color;
                _aux.a = listFadeText[i].color.a - fadeSpeed * Time.deltaTime;
                listFadeText[i].color = _aux;

                _hasFaded = false;
            }
            else
            {
                _aux = listFadeText[i].color;
                _aux.a = fadeMax;
                listFadeText[i].color = _aux;

                _timing = 0;
                _hasFaded = true;
            }
        }

        return _hasFaded;
    }
}
