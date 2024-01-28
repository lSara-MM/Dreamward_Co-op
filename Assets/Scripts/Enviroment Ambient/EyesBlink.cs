using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesBlink : MonoBehaviour
{
    float timing = 0f;
    public SpriteRenderer sprite;

    private float xScale;

    private void Start()
    {
        xScale = this.gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        timing += Time.deltaTime;

        if (timing > Random.Range(3f, 10f))
        {
            this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x - 8f * Time.deltaTime, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
            //sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - Time.deltaTime);
        }

        if(this.gameObject.transform.localScale.x < 0.001f) 
        { 
            timing = 0f;
            this.gameObject.transform.localScale = new Vector3(xScale, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
            //sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
        }
    }
}
