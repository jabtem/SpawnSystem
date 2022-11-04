using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class test : MonoBehaviour
{

    public SpawnPointObj t;

    public SpawnPoint t2;


    private void Update()
    {
        if(t != null)
        {
            t2.sgId = t.spawnPointData.sgId;
            t2.spId = t.spawnPointData.spId;
            t2.spawnPoint = t.spawnPointData.spawnPoint;
        }

    }
}
