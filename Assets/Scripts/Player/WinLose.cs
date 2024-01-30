using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLose : MonoBehaviour
{

    [Header("Win")]
    [SerializeField] private GameObject[] wToDisable;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private FadeToBlack fade;
    public bool _won;

    [Header("Lose")]
    [SerializeField] private GameObject[] lToDisable;
    [SerializeField] private GameObject loseCanvas;
    public bool _lost;

    // Start is called before the first frame update
    void Start()
    {
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_won)
        {
            if (fade.Fade())
            {
                OpenWin();
            }
        }

        if (_lost)
        {
            if (fade.Fade())
            {
                OpenLose();
            }
        }
    }
    public void OpenWin()
    {
        for (int i = 0; i < wToDisable.Length; ++i)
        {
            wToDisable[i].SetActive(false);
        }

        winCanvas.SetActive(true);
    }

    public void OpenLose()
    {
        for (int i = 0; i < lToDisable.Length; ++i)
        {
            lToDisable[i].SetActive(false);
        }

        loseCanvas.SetActive(true);
        _lost = false;
    }
}
