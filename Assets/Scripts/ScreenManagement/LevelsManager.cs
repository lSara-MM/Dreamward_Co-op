using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering.Universal;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private Light2D[] _lamps;
    [SerializeField] private BoolSO[] _lvl;

    [Header("Colors")]
    [SerializeField] private Color _normal = new Color(1f, 0.4239883f, 0f);
    [SerializeField] private Color _completed = new Color(0f, 0.6030378f, 1f);
    [SerializeField] private Color _nightmare = new Color(0.3703693f, 0f, 0.6679245f);

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _lamps.Length; ++i)
        {
            if (_lvl[i].Value)
            {
                _lamps[i].color = _completed;
            }
            else
            {
                _lamps[i].color = _normal;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeNightmare(bool night)
    {
        for (int i = 0; i < _lamps.Length; ++i)
        {
            if (night)
            {
                _lamps[i].color = _nightmare;
            }
            else
            {
                _lamps[i].color = _normal;
            }
        }
    }
}
