using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnSystemManager : MonoBehaviour
{
    [System.Serializable]
    public class SpawnCluster
    {
        public List<SpawnGroup> Sg = new();
        //This ID
        //자기 자신 ID
        public int scId;

        //Explanation
        //클러스터에대한 부가설명
        public string explanation;

    }

    [System.Serializable]
    public class SpawnGroup
    {
        public List<SpawnPoint> Sp = new();
        //This ID
        public int sgId;
        //Parent ID
        public int scId;

        public string monsterType;

        public int maxCount;
    }

    [System.Serializable]
    public class SpawnPoint
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

    }

    public List<SpawnCluster> spawnClusters;


}
