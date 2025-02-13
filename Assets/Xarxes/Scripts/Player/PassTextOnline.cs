using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassTextOnline : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Text")
        {
            collision.gameObject.GetComponent<PassText>().ResetFade(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Text")
        {
            collision.gameObject.GetComponent<PassText>().ResetFade(false);
        }
    }
}
