using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class SceneSelectorBell : MonoBehaviour
{
    public string PassScene;
    public SecondPhaseHub SceneManager;
    public FloatSO Dialogue;
    public int DialogueSelector; // 0 = Intro dialogue, 1 = Boss 1 dialogue, 2 = Boss 2 Dialogue, 3 = None

    public Rigidbody2D player;

    public AudioSource bellSound;

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
        // TODO: Go to boss scene
        //if (PassScene != null && collision.gameObject.tag == "Player")
        //{
        //    player.gravityScale = 2f;
        //    Dialogue.Value = DialogueSelector;
        //    bellSound.Play();
        //    SceneManager.scene = PassScene;
        //    SceneManager.secondPhase = true;
        //}
    }
}
