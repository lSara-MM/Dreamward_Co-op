using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class Globals
{
    static public List<GameObject> dontDestroyList = new List<GameObject>();

    static public void AddDontDestroy(GameObject item)
    {
        dontDestroyList.Add(item);
    }

    static public void StartNewThread(Action action)
    {
        Thread thread = new Thread(new ThreadStart(action));
        thread.Start();
    }
}
