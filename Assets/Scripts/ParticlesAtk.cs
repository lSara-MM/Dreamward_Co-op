using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormRainAtk : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private int _dmg = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Take Damage"); //Función para recibir daño
            _playerHealth.TakeDmg(_dmg);
        }
    }
}
