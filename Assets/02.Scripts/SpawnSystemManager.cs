using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnSystemManager : MonoBehaviour
{
    [System.Serializable]
    
    public class SpawnCluster
    {
        [Header("���� �׷�")]
        public List<SpawnGroup> Sg = new();
        //This ID
        //�ڱ� �ڽ� ID
        [Header("���� ID")]
        public int scId;

        //Explanation
        //Ŭ�����Ϳ����� �ΰ�����
        [Header("�ΰ� ����")]
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


    //���� ID�� ������ ��ID , ��ID + Ŭ������ID = JSON�� ����� Ŭ������ ID
    public int sceneId;

    public List<SpawnCluster> spawnClusters;


}
