using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public float smoothTime = 0.25f;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        target = GameObject.Find("Player Online Version").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movePosition = transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, movePosition, smoothTime);
    }
}
