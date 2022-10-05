using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions 
{
    public static T GetRandomItem<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
    public static void SetLocalEulerZ(this Transform t,float z)
    {
        t.eulerAngles = new Vector3(0,0,z);
    }
}
