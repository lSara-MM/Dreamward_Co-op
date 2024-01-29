using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float rechargeSpeed = 1;
    [SerializeField] private int debugStamina = 20;

    public float maxEnergy = 100;
    public float currentEnergy;

    public Text textEnergyBar;

    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = maxEnergy;

        slider.maxValue = maxEnergy;
        slider.minValue = 0;
        slider.value = currentEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnergy < maxEnergy)
        {
            currentEnergy = Mathf.Clamp(currentEnergy + rechargeSpeed * Time.deltaTime, 0, maxEnergy);
            slider.value = currentEnergy;
        }

        if (Input.GetKeyDown(KeyCode.F7))
        {
            Debug.Log("Use Debug Stamina");
            UseEnergy(debugStamina);
        }
    }

    public bool UseEnergy(float energy)
    {
        if (currentEnergy >= energy)
        {
            currentEnergy -= energy;
            //textEnergyBar.text = (int)currentEnergy + "/" + maxEnergy;
            slider.value = currentEnergy;
            return true;
        }

        return false;
    }
}
