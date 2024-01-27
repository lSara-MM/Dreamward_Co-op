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

    public Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    [SerializeField] private int debugLife = 1;

    //[SerializeField] private GameObject game;
    //[SerializeField] private GameObject loseCanvas;

    public AudioClip[] Clip;
    AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        //loseCanvas.SetActive(false);

        aud = GetComponent<AudioSource>();
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
    }

    public void TakeDmg(int dmg_)
    {
       currentHealth = Mathf.Clamp(currentHealth - dmg_, 0, maxHealth);
    
        if (currentHealth > 0)
        {
            // player hurt
            if (currentHealth <= maxInitHealth)
            {
                maxHealth = maxInitHealth;

                //AudioPlay(Clip[0]);
            }
        }
        else
        {
            // player dead
            OpenLose();
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
        //game.SetActive(false);
        //loseCanvas.SetActive(true);
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
