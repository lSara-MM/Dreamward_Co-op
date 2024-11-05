using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals
{
    static public List<GameObject> dontDestroyList = new List<GameObject>();

    static public void AddDontDestroy(GameObject item)
    {
        dontDestroyList.Add(item);
    }
}
