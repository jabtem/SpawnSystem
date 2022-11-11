using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using UnityEngine.Networking;
using Assets.Charon.CharonData;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{

    SpawnClusterContainer container;

    static string dataPath = $"{Application.streamingAssetsPath}/SpawnInfo.gdjs";
    static string jsonPath;

    SpawnInfo data;

    SpawnData readJsonData;


    private void Awake()
    {
        jsonPath = $"Spawndata/{SceneManager.GetActiveScene().name}SpawnData";
        var jsonData = Resources.Load<TextAsset>(jsonPath);
        readJsonData = JsonUtility.FromJson<SpawnData>(jsonData.ToString());

        //TryGetComponent<SpawnClusterContainer>(out container);

        

    }

    IEnumerator Start()
    {
        WebGlDataRead(dataPath);

        while (data is null)
        {
            yield return new WaitForEndOfFrame();
        }

        SpawnReqeust();
    }

    private void WebGlDataRead(string path)
    {
        StartCoroutine(CoWebGlDataRead(path));
    }

    private IEnumerator CoWebGlDataRead(string path)
    {
        UnityWebRequest uri = UnityWebRequest.Get(path);

        yield return uri.SendWebRequest();


        byte[] uriData = uri.downloadHandler.data;


        MemoryStream stream = new(uriData);

        data = new SpawnInfo(stream, SpawnInfo.Format.Json);


    }


    void SpawnReqeust()
    {
        foreach(var data in data.GetSpawnDatas())
        {
            SpawnOrder(data.ScId, data.SgId);
        }
    }

    public void SpawnOrder(int cid, int gid)
    {
        foreach (var cluster in readJsonData.spawnClusters)
        {
            if (cluster.scId == cid)
            {
                foreach (var group in cluster.SgObj)
                {
                    if (group.spawnGroupData.sgId == gid)
                    {

                        //group.SpawnStart();
                        group.spawnGroupData.spawnStart = true;
                        //if(group.spawnRandom)
                        //{
                        //    RandomSpawn(group);
                        //}
                        //else
                        //{
                        //    //foreach (var point in group.Sp)
                        //    //{
                        //    //    if (point.spId == pid)
                        //    //    {
                        //    //        SpawnMonster(group.monsterType, point.spawnPoint);

                        //    //    }
                        //    //}
                        //}


                    }
                }
            }
        }
    }

    

}
