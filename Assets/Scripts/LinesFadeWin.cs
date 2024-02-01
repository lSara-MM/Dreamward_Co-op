using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinesFadeWin : MonoBehaviour
{
    public SpriteRenderer render;

    // Start is called before the first frame update
    void Start()
    {
        render.color = new Color(1f, 1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(render.color.a > 0f)
        {
            render.color = new Color(render.color.r, render.color.g, render.color.b, render.color.a - 0.3f * Time.deltaTime);
        }
    }
}
