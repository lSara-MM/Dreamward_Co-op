using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextShow : MonoBehaviour
{
    public Text text;
    public bool isVisible = false;
    public float timing = 0f;

    public SecondPhaseHub sceneManager;

    // Start is called before the first frame update
    void Start()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        timing += Time.deltaTime;

        if(isVisible && text.color.a < 1f && !sceneManager.secondPhase) 
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + Time.deltaTime);
        }
        
        if(!isVisible && text.color.a > 0f && !sceneManager.secondPhase) 
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            timing = 0f;
            isVisible = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            timing = 0f;
            isVisible = false;
        }
    }
}
