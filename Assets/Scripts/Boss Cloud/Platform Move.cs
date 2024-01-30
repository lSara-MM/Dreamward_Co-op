using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlatformMove : MonoBehaviour
{
    enum positions 
    {
        LEFT,
        MIDDLE,
        RIGHT,
    }
    [SerializeField] positions positionActual;
    [SerializeField] float time = 0.0f;
    [SerializeField] int direction = 0;
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;

        //Se podria hacer random
        positionActual = positions.MIDDLE;
        switch (positionActual) 
        {
            case positions.LEFT:
            {
                transform.position = new Vector3(-7.0f, -1.5f, 0.0f);
                direction = 1;
                break;
            }
            case positions.MIDDLE:
            {
                transform.position = new Vector3(0.0f, -1.5f, 0.0f);
                direction = 1;
                break;
            }
            case positions.RIGHT:
            {
                transform.position = new Vector3(7.0f, -1.5f, 0.0f);
                direction = -1;
                break;
            }
        }
    }

    bool lerping = false;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (lerping) 
        {
        
        }
        else 
        {
            if (time > 0.0f) { }
        }
    }

    Vector3 Lerping() 
    {
        return Vector3.zero;
    }
}
