using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpawnPointObj : MonoBehaviour
{


    public SpawnPoint spawnPointData;

    private void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR

        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(transform.position, 0.5f);

        var oldColor = UnityEditor.Handles.color;
        var color = Color.blue;
        color.a = 0.1f;
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.DrawSolidDisc(this.transform.position, Vector3.up, spawnPointData.radius);
        UnityEditor.Handles.color = oldColor;

        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawSolidDisc(this.transform.position, Vector3.up, 0.1f);
        UnityEditor.Handles.color = oldColor;

#endif
    }

    private void Update()
    {
        if(spawnPointData != null && transform.hasChanged)
        {
            spawnPointData.spawnPoint = transform.position;
        }
    }

    private void OnValidate()
    {
        SpawnClusterContainer.Instance.DataRefresh();
    }
}
