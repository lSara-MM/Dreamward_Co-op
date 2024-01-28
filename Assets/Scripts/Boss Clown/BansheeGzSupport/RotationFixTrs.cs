using UnityEngine;
using System.Collections;
using BansheeGz.BGSpline.Components;

[RequireComponent(typeof(BGCcTrs))]
public class RotationFixTrs : MonoBehaviour
{
    private BGCcTrs trs;
    private bool speedIsPositive;

    // Use this for initialization
    void Start()
    {
        trs = GetComponent<BGCcTrs>();
        speedIsPositive = trs.Speed > 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (speedIsPositive && trs.SpeedIsReversed)
        {
            speedIsPositive = false;
            trs.OffsetAngle = new Vector3(0,180,0);
        }
        else if (!speedIsPositive && !trs.SpeedIsReversed)
        {
            speedIsPositive = true;
            trs.OffsetAngle = Vector3.zero;
        }
    }
}