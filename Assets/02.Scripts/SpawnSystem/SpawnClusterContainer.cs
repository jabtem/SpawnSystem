using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.AddressableAssets;
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


    [JsonIgnore]
    [System.NonSerialized]
    public bool isClick;
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
        Sp = new();
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
    public int sceneId;

    public List<SpawnCluster> spawnClusters;

    public SpawnData spawnData;


    public void SpawnOrder(int cid,int gid, int pid)
    {
        foreach(var cluster in spawnClusters)
        {
            if(cluster.scId == cid)
            {
                foreach(var group in cluster.Sg)
                {
                    if (group.sgId == gid)
                    {
                        foreach (var point in group.Sp)
                        {
                            if(point.spId == pid)
                            {
                                SpawnMonster(group.monsterType, point.spawnPoint);

                            }
                        }
                    }
                }
            }
        }
    }



    public void SpawnMonster(string type, Vector3 pos)
    {
        StartCoroutine(CoSpawnMonster(type, pos));
    }

    IEnumerator CoSpawnMonster(string type, Vector3 pos)
    {
        var handle = Addressables.InstantiateAsync(type);
        handle.Completed += (data) =>
        {
            data.Result.transform.position = pos;
        };

        yield return handle;
    }
}