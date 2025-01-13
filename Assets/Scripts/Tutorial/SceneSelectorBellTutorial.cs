using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelectorBellTutorial : MonoBehaviour
{
    public string PassScene;
    public PassTutorial passTutorial;
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
        if (PassScene != null && collision.gameObject.tag == "Player")
        {
            bellSound.Play();
            passTutorial.scene = PassScene;
            passTutorial.secondPhase = true;

            DeletePlayers();
        }
    }

    public void DeletePlayers()
    {
        GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");

        FunctionsToExecute cs_functionsToExecute = GameObject.FindGameObjectWithTag("Serialization").GetComponent<FunctionsToExecute>();

        foreach (GameObject item in playerList)
        {
            // Remove the guid from the dictionary
            cs_functionsToExecute.guidDictionary.Remove(
                Globals.FindKeyByValue(cs_functionsToExecute.guidDictionary, item));

            // Remove from don't destroy list so it gets recreated
            Globals.dontDestroyList.Remove(item);
            Destroy(item);

            Debug.Log("ITEM DESTROYED " + item.gameObject.name);
        }
    }
}
