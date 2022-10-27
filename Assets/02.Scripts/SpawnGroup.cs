using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGroup : MonoBehaviour
{
    public List<SpawnPoint> Sp = new();
    //This ID
    public int sgId;
    //Parent ID
    public int scId;

    public string monsterType;

    public int maxCount;

    public SpawnGroup(int _scid)
    {
        scId = _scid;
    }
}
