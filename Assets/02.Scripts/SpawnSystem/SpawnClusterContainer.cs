using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class SpawnCluster
{
    [HideInInspector]
    public List<SpawnGroup> Sg = new();
    [Header("���� �׷�")]
    public List<SpawnGroupObj> SgObj = new();

    //This ID
    //�ڱ� �ڽ� ID
    [Header("���� ID")]
    public int scId;

    //Explanation
    //Ŭ�����Ϳ����� �ΰ�����
    [Header("�ΰ� ����")]
    public string explanation;


    //JsonConvet Ignore
    [JsonIgnore]
    //JsonUtility Ignore
    [System.NonSerialized]
    public bool isClick;


    [HideInInspector]
    public GameObject clusterObj;
}
[System.Serializable]
public class SpawnGroup
{
    [HideInInspector]
    public List<SpawnPoint> Sp = new();

    [Header("���� ����Ʈ")]
    public List<SpawnPointObj> SpObj = new();

    
    //This ID
    public int sgId;

    //Parent ID
    public int scId;

    public string monsterType;

    public int maxCount;

    public bool spawnRandom;

    public SpawnGroup(int _scid)
    {
        scId = _scid;
        Sp = new();
    }

}
[System.Serializable]
public class SpawnPoint
{
    [Header("����Ʈ ID")]
    //This ID
    public int spId;

    //Parent ID;
    public int sgId;

    //SpawnPoint
    public Vector3 spawnPoint;

    [Header("���� ���� �ݰ�")]
    public int radius;
    [Header("���� ���� ����")]
    public bool isRandom;

    public SpawnPoint(int _SgId, Vector3 point)
    {
        sgId = _SgId;
        spawnPoint = point;
    }

}
[System.Serializable]
public class SpawnData
{
    public List<SpawnCluster> spawnClusters;
}


public class SpawnClusterContainer : MonoBehaviour
{
    //���� ID�� ������ ��ID , ��ID + Ŭ������ID = JSON�� ����� Ŭ������ ID

    SpawnClusterContainer()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public static SpawnClusterContainer Instance
    {
        get => instance;
    }

    static SpawnClusterContainer instance;

    public int sceneId;

    public List<SpawnCluster> spawnClusters = new();

    public SpawnData spawnData;


    public void DataRefresh()
    {
        foreach (var cluster in spawnClusters)
        {
            for(int i= 0; i< cluster.Sg.Count; ++i)
            {
                cluster.SgObj[i].spawnGroupData.scId = cluster.scId;

                cluster.Sg[i] = cluster.SgObj[i].spawnGroupData;
                for(int j = 0; j < cluster.Sg[i].Sp.Count; ++j)
                {
                    cluster.Sg[i].SpObj[j].spawnPointData.sgId = cluster.Sg[i].sgId;
                    cluster.Sg[i].Sp[j] = cluster.Sg[i].SpObj[j].spawnPointData;
                }
            }
        }
    }
}
