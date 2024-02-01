using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FaceCloudBlink : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Color colorToChange;
    [SerializeField] private BossHealth vidaBoss;
    private Color originalSpriteColor;
    float timeDT = 0.0f;
    [SerializeField] private float changeDuration = 0.5f; 
    bool startCount = false;

    // Start is called before the first frame update
    void Start()
    {
        originalSpriteColor = sprite.color; //Servira esto? Quiza tengo que guardar algo mas complejo
    }

    // Update is called once per frame
    void Update()
    {
        if (startCount) { timeDT += Time.deltaTime; }
        
        if (vidaBoss._hitBoss) 
        {
            sprite.color = colorToChange;
            startCount = true; //Timer de que se quede printado
        }
        else if(timeDT>0.5f)
        {
            sprite.color = originalSpriteColor;
            timeDT = 0.0f;
            startCount = false;
        }
    }
}
