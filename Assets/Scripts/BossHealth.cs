using ns_structure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public Animator animator;
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
    public bool _hitBoss = false;

    // Time to send current boss health packet
    public float timerHealth = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Teleport players to (0,0,0)
        foreach (GameObject item in Globals.dontDestroyList)
        {
            if (item.tag == "Player")
            {
                item.GetComponent<PlayerOnline>().ResetPlayer();
            }
        }

        transform.position = new Vector3(-4.743f, -10.727f, 0);

        GameObject go = GameObject.Find("Game");
        _winLose = go.GetComponent<WinLose>();

        animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        slider = GameObject.Find("BossHealth").GetComponent<Slider>();

        slider.maxValue = maxHealth;
        slider.minValue = 0;
        slider.value = currentHealth;
        _hitBoss = false;

        GameObject.Find("SceneManager").GetComponent<SecondPhase>().AssignSecondPhase();

        if (GameObject.FindGameObjectWithTag("Server") != null)
        {
            StartCoroutine(UpdateHealth());
        }
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

        SendBossHealth(currentHealth);

        Time.timeScale = _bulletTime;
        _hitBoss = true;

        if (currentHealth > 0)
        {
            // boss hurt
        }
        else
        {
            // boss dead
            Death();
        }
    }

    public void Death()
    {
        // boss dead
        currentHealth = 0;
        animator.SetTrigger("Death");
        _winLose._won = true;
    }

    public void SendBossAttack(int num)
    {
        Serialization cs_Serialization = GameObject.FindGameObjectWithTag("Serialization").GetComponent<Serialization>();
        cs_Serialization.SerializeData(GetComponent<GUID_Generator>().GetGuid(), ACTION_TYPE.BOSS_ATTACK, num);
    }

    public void SendBossMovement(vector2D position)
    {
        Serialization cs_Serialization = GameObject.FindGameObjectWithTag("Serialization").GetComponent<Serialization>();
        cs_Serialization.SerializeData(GetComponent<GUID_Generator>().GetGuid(), ACTION_TYPE.BOSS_MOVEMENT, position);
    }

    public void SendBossHealth(int num)
    {
        Serialization cs_Serialization = GameObject.FindGameObjectWithTag("Serialization").GetComponent<Serialization>();
        cs_Serialization.SerializeData(GetComponent<GUID_Generator>().GetGuid(), ACTION_TYPE.BOSS_HEALTH, currentHealth);
    }

    private IEnumerator UpdateHealth()
    {
        while (currentHealth > 0)
        {
            SendBossHealth(currentHealth);
            yield return new WaitForSeconds(timerHealth);
        }
    }

    #region Boss NPC debug
    // Function to call when packet of the attack chosen is sent
    public void ChooseAttack(int attack = 0)
    {
        animator.SetInteger("ChooseAttack", attack);
    }

    // Function to call when packet of the target position is sent
    public void ChooseTarget(int target = 0)
    {
        animator.GetBehaviours<BossMoveNPC>()[0].target = target;
        animator.GetBehaviours<BossMoveNPC>()[0].targetSelected = true;
    }
    #endregion // Boss NPC
}
