using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    private Animator _animator;
    public bool bossSP = false; // Boss second phase

    [SerializeField] private Slider slider;
    [SerializeField] private int debugLife = 20;

    [Header("HP")]
    public int currentHealth = 300;
    public int maxHealth = 500;

    [SerializeField] private WinLose _winLose;

    private float _timer = 0f;
    [SerializeField] float _delayBullet = 0.5f;
    [SerializeField] float _bulletTime = 0.7f;
    private bool _hitBoss = false;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        slider.maxValue = maxHealth;
        slider.minValue = 0;
        slider.value = currentHealth;
        _hitBoss = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth <= maxHealth / 2 && bossSP == false && !_winLose._won)
        {
            bossSP = true;
            Debug.Log("SA ENFADAO");
        }

        slider.value = currentHealth;

        // Debug
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Debug.Log("Dmg boss");
            TakeDmg(debugLife);
        }
        // Reset all
        if (Input.GetKeyDown(KeyCode.F4))
        {
            currentHealth = maxHealth;
        }
        // Kill boss
        if (Input.GetKeyDown(KeyCode.F6))
        {
            Debug.Log("Kill boss");
            TakeDmg(maxHealth);
        }

        if (_hitBoss)
        {
            _timer += Time.deltaTime;

            if (_timer >= _delayBullet)
            {
                _timer = 0;
                Time.timeScale = 1;
                _hitBoss = false;
            }
        }
    }

    public void TakeDmg(int dmg_)
    {
        currentHealth = Mathf.Clamp(currentHealth - dmg_, 0, maxHealth);

        Time.timeScale = _bulletTime;
        _hitBoss = true;

        if (currentHealth > 0)
        {
            // boss hurt
        }
        else
        {
            // boss dead
            currentHealth = 0;
            Death();
            _winLose._won = true;
        }
    }

    public void Death()
    {
        // boss dead
        _animator.SetTrigger("Death");
    }

    public void OpenWin()
    {
        //game.SetActive(false);
        //winCanvas.SetActive(true);
    }
}
