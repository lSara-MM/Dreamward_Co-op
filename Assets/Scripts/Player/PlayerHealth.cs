using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private int debugLife = 1;
    private Blink _blink;

    [SerializeField] private GameObject game;
    [SerializeField] private GameObject loseCanvas;
    [SerializeField] private FadeToBlack fade;
    private bool _lost;

    public AudioClip[] Clip;
    AudioSource aud;

    //i-frame
    private bool _isInvuln = false;
    private float _timer = 0;
    [SerializeField] private float _invulnTime = 1f;

    private PlayerMovement _move;

    private Animator _bossAnimator;
    private GameObject _boss;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        loseCanvas.SetActive(false);

        _blink = GetComponent<Blink>();
        _move = GetComponent<PlayerMovement>();
        aud = GetComponent<AudioSource>();

        // Dificulty Selector
        dificultySelector = GameObject.Find("DifultySelector");

        if (dificultySelector != null)
        {
            dificultySelectorScript = dificultySelector.GetComponent<DificultySelector>();
        }

        if (dificultySelectorScript.hardMode)
        {
            currentHealth = 1;
            maxHealth = 1;
            maxInitHealth = 1;
        }

        _boss = GameObject.Find("Enemy").gameObject;
        _bossAnimator = _boss.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayHealth();

        // Debug
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("Dmg player");
            TakeDmg(debugLife);
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

        if (_isInvuln)
        {
            _timer += Time.deltaTime;
            if (_timer >= _invulnTime)
            {
                _isInvuln = false;
                _timer = 0;
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

    public void TakeDmg(int dmg_)
    {
        if (!_isInvuln && !_move.isDashing)
        {
            currentHealth = Mathf.Clamp(currentHealth - dmg_, 0, maxHealth);

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
                // player dead            
                //OpenLose();
                _lost = true;
                _bossAnimator.SetTrigger("BossWins");
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

    public void OpenLose()
    {
        game.SetActive(false);
        loseCanvas.SetActive(true);
        _lost = false;
    }

    void AudioPlay(AudioClip a)
    {
        aud.clip = a;
        aud.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7) //Layer Damage = 7
        {
            if (!PlayerMovement.isDashing) //Si no está Dasheando recibe daño
            {
                Debug.Log("Take Damage"); //Función para recibir daño 
                TakeDmg(1);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) //Layer Damage = 7
        {
            if (!PlayerMovement.isDashing) //Si no está Dasheando recibe daño
            {
                Debug.Log("Take Damage"); //Función para recibir daño 
                TakeDmg(1);
            }
        }
    }
}