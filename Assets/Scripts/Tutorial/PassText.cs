using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassText : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image[] listFadeImg;
    [SerializeField] private Text[] listFadeText;

    [Header("Scripts")]
    [SerializeField] private UIFadeFromBlack fadeFromBlack;
    [SerializeField] private UIFadeToBlack fadeToBlack;

    public bool isVisible = false;
    public PassTutorial passTutorial;

    // Start is called before the first frame update
    void Start()
    {
        fadeFromBlack = GetComponent<UIFadeFromBlack>();
        fadeToBlack = GetComponent<UIFadeToBlack>();

        fadeFromBlack.listFadeImg = listFadeImg;
        fadeFromBlack.listFadeText = listFadeText;

        fadeToBlack.listFadeImg = listFadeImg;
        fadeToBlack.listFadeText = listFadeText;

        fadeToBlack.SetAlpha(0);
    }

    // Update is called once per frame
    void Update()
    {
        // Fade from black
        if (isVisible && !passTutorial.secondPhase)
        {
            fadeFromBlack.UnFadeUI();
        }

        // Fade to black
        if (!isVisible && !passTutorial.secondPhase)
        {
            fadeToBlack.FadeUI();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            fadeFromBlack.timing = 0f;
            fadeToBlack.timing = 0f;
            isVisible = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            fadeFromBlack.timing = 0f;
            fadeToBlack.timing = 0f;
            isVisible = false;
        }
    }
}
