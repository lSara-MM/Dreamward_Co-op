using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUI : MonoBehaviour
{
    [SerializeField] private Transform lookAt;
    [SerializeField] private Vector3 offset;
    [SerializeField] Camera cam;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(lookAt.position + offset);

        if (transform.position != pos) 
        { 
            transform.position = pos;
        }
    }
}
