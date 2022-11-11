using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    SpawnGroup parentGroup;

    private void OnDestroy()
    {
        --parentGroup.SpawnCount;
    }

    public void parentSet(SpawnGroup group)
    {
        parentGroup = group;
    }

}
