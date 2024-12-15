using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public static class Globals
{
    #region Don't destroy GameObjects
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
    #endregion //Don't destroy GameObjects

    static public T FindKeyByValue<T, W>(this Dictionary<T, W> dict, W val)
    {
        T key = default;
        foreach (KeyValuePair<T, W> pair in dict)
        {
            if (EqualityComparer<W>.Default.Equals(pair.Value, val))
            {
                key = pair.Key;
                break;
            }
        }
        return key;
    }
}