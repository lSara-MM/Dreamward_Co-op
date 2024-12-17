using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth = 0;
    public int maxHealth = 3;
    public int maxInitHealth = 3;
    public PlayerMovement PlayerMovement;

    public GameObject dificultySelector;
    public DificultySelector dificultySelectorScript;

    public Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    [Header("Debug")]
    [SerializeField] private bool _godMode = false;
    [SerializeField] private int _debugLife = 1;

    private Blink _blink;

    [SerializeField] private WinLose _winLose;

    [Header("Audio")]
    public AudioClip[] Clip;
    AudioSource aud;

    //i-frame
    private bool _isInvuln = false;
    private float _timer = 0;
    [SerializeField] private float _invulnTime = 1f;

    private PlayerMovement _move;

    public Animator bossAnimator;

    [SerializeField] private float _shakeIntensity = 5;
    [SerializeField] private float _shakeFrequency = 5;
    [SerializeField] private float _shakeTime = 0.5f;

    private Animator _animator;

    [SerializeField] private GameObject healthUI;

    // Start is called before the first frame update
    void Start()
    {
        _blink = GetComponent<Blink>();
        _move = GetComponent<PlayerMovement>();
        aud = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayHealth();

        // Debug
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("Dmg player");
            TakeDmg(_debugLife);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Debug.Log("God mode");
            _godMode = !_godMode;
        }
        // Reset all
        if (Input.GetKeyDown(KeyCode.F4))
        {
            currentHealth = maxHealth;
        }
        // Kill player
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Debug.Log("Kill player");
            TakeDmg(maxHealth);
        }
        // Return to hub
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("Return to hub");


            ChangeToScene("Hub");
        }

        if (_isInvuln)
        {
            _timer += Time.deltaTime;
            if (_timer >= _invulnTime)
            {
                _isInvuln = false;
                _timer = 0;
            }
        }
    }

    // TODO: Remove this function
    public void ChangeToScene(string passToScene)
    {
        Debug.Log("Change Scene " + passToScene);

        GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");

        FunctionsToExecute cs_functionsToExecute = GameObject.FindGameObjectWithTag("Serialization").GetComponent<FunctionsToExecute>();

        foreach (GameObject item in playerList)
        {
            // Remove the guid from the dictionary
            cs_functionsToExecute.guidDictionary.Remove(
                Globals.FindKeyByValue(cs_functionsToExecute.guidDictionary, item));

            // Remove from don't destroy list so it gets recreated
            Globals.dontDestroyList.Remove(item);
            Destroy(item);
        }

        foreach (GameObject item in Globals.dontDestroyList)
        {
            DontDestroyOnLoad(item);
        }

        Serialization cs_Serialization = GameObject.FindGameObjectWithTag("Serialization").GetComponent<Serialization>();
        cs_Serialization.SerializeData(default, ACTION_TYPE.CHANGE_SCENE, passToScene);

        SceneManager.LoadScene(passToScene);
    }

    public void TakeDmg(int dmg_)
    {
        if (!GetComponent<PlayerOnline>().isNPC)
        {
            if (!_godMode && !_isInvuln && !_move.isDashing)
            {
                currentHealth = Mathf.Clamp(currentHealth - dmg_, 0, maxHealth);

                CameraShake.Instance.ShakeCamera(_shakeIntensity, _shakeFrequency, _shakeTime);

                if (currentHealth > 0)
                {
                    // player hurt
                    _blink.Flash();
                    _isInvuln = true;

                    if (currentHealth <= maxInitHealth)
                    {
                        maxHealth = maxInitHealth;

                        //AudioPlay(Clip[0]);
                    }
                }
                else
                {
                    Serialization cs_Serialization = GameObject.FindGameObjectWithTag("Serialization").GetComponent<Serialization>();
                    GUID_Generator cs_guid = gameObject.GetComponent<GUID_Generator>();

                    cs_Serialization.SerializeData(cs_guid.GetGuid(), ACTION_TYPE.PLAYER_DEATH, true);

                    // player dead
                    GetComponent<PlayerDeath>().Death();
                }
            }
        }
    }

    private void DisplayHealth()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }


            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    void AudioPlay(AudioClip a)
    {
        aud.clip = a;
        aud.Play();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7) //Layer Damage = 7
        {
            //Debug.Log("Take Damage"); //Función para recibir daño 
            TakeDmg(1);
        }

        if (collision.gameObject.layer == 11) //Layer Death = 11
        {
            Debug.Log("Die"); //Función para recibir daño 
            TakeDmg(currentHealth);
            if (_godMode) { transform.position = new Vector3(0, 0, transform.position.z); }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) //Layer Damage = 7
        {
            {
                Debug.Log("Take Damage"); //Función para recibir daño 
                TakeDmg(1);
            }
        }
    }

    public void AssignPlayerHealth()
    {
        currentHealth = maxHealth;
        _winLose = GameObject.Find("Game").GetComponent<WinLose>();

        GameObject boss = GameObject.FindWithTag("Boss").gameObject;

        if (boss != null)
        {
            bossAnimator = boss.GetComponent<Animator>();
        }

        if (healthUI != null)
        {
            for (int i = 0; i < healthUI.transform.childCount; i++)
            {
                hearts[i] = healthUI.transform.GetChild(i).gameObject.GetComponent<Image>();

                hearts[i].color = GetComponent<PlayerOnline>().GetPlayerData().GetColorColor();
            }
        }
    }
}