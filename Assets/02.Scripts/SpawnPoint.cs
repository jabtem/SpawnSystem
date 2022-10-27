using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    //This ID
    public int spId;

    //Parent ID;
    public int sgId;

    //SpawnPoint
    public Vector3 spawnPoint;


    public int radius;

    public bool isRandom;

    public SpawnPoint(int _Spid, int _SgId, Vector3 point)
    {
        spId = _Spid;
        sgId = _SgId;
        spawnPoint = point;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 1f);
    }
}
