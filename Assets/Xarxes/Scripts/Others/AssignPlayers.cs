using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignPlayers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Teleport players to (0,0,0)
        foreach (GameObject item in Globals.dontDestroyList)
        {
            if (item.tag == "Player")
            {
                item.GetComponent<PlayerOnline>().ResetPlayer();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
