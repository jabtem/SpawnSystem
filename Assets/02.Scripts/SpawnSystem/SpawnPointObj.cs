using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpawnPointObj : MonoBehaviour
{


    public SpawnPoint spawnPointData;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 0.5f);

    }

    private void Update()
    {
        if(spawnPointData != null && transform.hasChanged)
        {
            spawnPointData.spawnPoint = transform.position;
        }
    }

}
