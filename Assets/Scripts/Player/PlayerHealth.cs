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

    private Animator _bossAnimator;
    public GameObject _boss;

    [SerializeField] private float _shakeIntensity = 5;
    [SerializeField] private float _shakeFrequency = 5;
    [SerializeField] private float _shakeTime = 0.5f;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        _blink = GetComponent<Blink>();
        _winLose = GetComponent<WinLose>();
        _move = GetComponent<PlayerMovement>();
        aud = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();

        // Dificulty Selector
        dificultySelector = GameObject.Find("DifultySelector");

        if (dificultySelector != null)
        {
            dificultySelectorScript = dificultySelector.GetComponent<DificultySelector>();

            if (dificultySelectorScript.hardMode)
            {
                currentHealth = 1;
                maxHealth = 1;
                maxInitHealth = 1;
            }
        }

        //_boss = GameObject.Find("Enemy").gameObject; // Andreu ni lo toques

        if (_boss != null)
        {
            _bossAnimator = _boss.GetComponent<Animator>(); // Andreu ni lo toques
        }
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

    public void TakeDmg(int dmg_)
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
                // player dead
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false; // Nose si desactivo player entero se rompe
                this.gameObject.GetComponent<Collider2D>().enabled = false; // Avoid collisions after death
                this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                _animator.SetTrigger("Death");
                _winLose._lost = true;
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

    void AudioPlay(AudioClip a)
    {
        aud.clip = a;
        aud.Play();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7) //Layer Damage = 7
        {
            Debug.Log("Take Damage"); //Función para recibir daño 
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
}