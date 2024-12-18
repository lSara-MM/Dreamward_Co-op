using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUI : MonoBehaviour
{
    [SerializeField] private Transform lookAt;
    [SerializeField] private Vector3 offset;
    private Camera cam;
    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        AssignCam();
    }

    // Update is called once per frame
    void Update()
    {
        if (cam != null)
        {
            pos = cam.WorldToScreenPoint(lookAt.position + offset);
        }

        if (transform.position != pos) 
        { 
            transform.position = pos;
        }
    }

    public void AssignCam() 
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
}
