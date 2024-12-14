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

    [SerializeField] private BoolSO lvl;

    [Header("Lose")]
    [SerializeField] private GameObject[] lToDisable;
    [SerializeField] private GameObject loseCanvas;
    public bool _lost;

    private Animator _bossAnimator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_won)
        {
            if (_bossAnimator.GetCurrentAnimatorStateInfo(0).IsName("Finish"))
            {
                if (fade.Fade())
                {
                    OpenWin();
                }
            }
        }

        if (_lost)
        {
            if (_bossAnimator.GetCurrentAnimatorStateInfo(0).IsName("Finish"))
            {
                if (fade.Fade())
                {
                    OpenLose();
                }
            }
        }
    }

    public void OpenWin()
    {
        DeletePlayers();

        for (int i = 0; i < wToDisable.Length; ++i)
        {
            wToDisable[i].SetActive(false);
        }

        winCanvas.SetActive(true);
    }

    public void OpenLose()
    {
        DeletePlayers();

        for (int i = 0; i < lToDisable.Length; ++i)
        {
            lToDisable[i].SetActive(false);
        }

        loseCanvas.SetActive(true);
        _lost = false;
    }

    public void AssignWinLose()
    {
        if (GameObject.Find("Enemy") != null)
        {
            _bossAnimator = GameObject.Find("Enemy").transform.gameObject.GetComponent<Animator>();
        }

        if (GameObject.Find("YouWin") != null)
        {
            winCanvas = GameObject.Find("YouWin");
            winCanvas.SetActive(false);
        }

        if (GameObject.Find("YouLose") != null)
        {
            loseCanvas = GameObject.Find("YouLose");
            loseCanvas.SetActive(false);
        }
    }

    public void DeletePlayers()
    {
        GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject item in playerList)
        {
            Globals.dontDestroyList.Remove(item);
            Destroy(item);
        }
    }
}
