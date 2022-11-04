using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class SpawnGroupObj : MonoBehaviour
{



    public SpawnGroup spawnGroupData;

    private void OnDrawGizmosSelected()
    {
        for (int i = 1; i < spawnGroupData.Sp.Count + 1; ++i)
        {
            Gizmos.color = Color.red;

            int j = i % spawnGroupData.Sp.Count;
            Gizmos.DrawLine(spawnGroupData.Sp[i - 1].spawnPoint, spawnGroupData.Sp[j].spawnPoint);
        }
    }

    private void OnValidate()
    {
        SpawnClusterContainer.Instance.DataRefresh();
    }
}
