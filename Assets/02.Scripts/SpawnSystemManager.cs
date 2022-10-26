using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnSystemManager : MonoBehaviour
{
    [System.Serializable]
    
    public class SpawnCluster
    {
        [Header("스폰 그룹")]
        public List<SpawnGroup> Sg = new();
        //This ID
        //자기 자신 ID
        [Header("스폰 ID")]
        public int scId;

        //Explanation
        //클러스터에대한 부가설명
        [Header("부가 설명")]
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

        public SpawnGroup(int _scid)
        {
            scId = _scid;
        }
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


    //각각 ID와 합쳐질 씬ID , 씬ID + 클러스터ID = JSON에 저장될 클러스터 ID
    public int sceneId;

    public List<SpawnCluster> spawnClusters;


}
