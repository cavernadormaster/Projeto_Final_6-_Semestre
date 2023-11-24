using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    public static Vector3 GetRandomSpawnPoint()
    {
        return new Vector3(Random.Range(41.5f, -41.7f), 4, Random.Range(-38f, 40.96f));
    }
}
