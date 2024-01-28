using UnityEngine;
using System.Collections;
using BansheeGz.BGSpline.Components;

[RequireComponent(typeof(BGCcCursorChangeLinear))]
public class RotationFixCursor : MonoBehaviour
{
    private BGCcCursorChangeLinear changeLinear;
    private BGCcCursorObjectRotate rotate;

    void Start()
    {
        changeLinear = GetComponent<BGCcCursorChangeLinear>();
        rotate = GetComponent<BGCcCursorObjectRotate>();

        changeLinear.PointReached += PointReached;
    }

    private void OnDestroy()
    {
        changeLinear.PointReached -= PointReached;
    }

    private void PointReached(object sender, BGCcCursorChangeLinear.PointReachedArgs e)
    {
        if (e.PointIndex == 0) rotate.OffsetAngle = Vector3.zero;
        else if (e.PointIndex == changeLinear.Curve.PointsCount - 1) rotate.OffsetAngle = new Vector3(0, 180, 0);
    }
}