using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint2;
    public Transform anchorPos;
    public Vector2 ancPos;

    // Start is called before the first frame update
    void Start()
    {
        distanceJoint2.enabled = false;
        ancPos = new Vector2(anchorPos.position.x, anchorPos.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, ancPos);
        lineRenderer.SetPosition(1, transform.position);
        distanceJoint2.connectedAnchor = ancPos;
        distanceJoint2.enableCollision = true;
        lineRenderer.enabled = true;

        if (distanceJoint2.enabled)
        {
            lineRenderer.SetPosition(1, transform.position);
        }
    }
}
