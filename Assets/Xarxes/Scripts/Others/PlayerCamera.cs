using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = GameObject.Find("Player Online Version").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
