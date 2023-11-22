using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    public static Vector3 GetRandomSpawnPoint()
    {
        return new Vector3(Random.Range(387.1068f, 387.1060f), 4, Random.Range(-462.3370f, -462.3372f));
    }
}
