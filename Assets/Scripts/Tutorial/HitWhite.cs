using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class HitWhite : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Color ogColor;
    private float timing = 0f;
    public float hittedTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>(); 
        
        if (sprite != null)
            ogColor = sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (sprite != null)
        {
            if (sprite.color == Color.white)
            {
                timing += Time.deltaTime;

                if (timing > hittedTime)
                {
                    sprite.color = ogColor;
                }
            }
        }
    }

    public void DoHitWhite()
    {
        sprite.color = Color.white;
        timing = 0;
    }
}
