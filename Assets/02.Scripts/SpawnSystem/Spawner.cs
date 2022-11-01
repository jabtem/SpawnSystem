using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Assets.Charon.CharonData;
public class Spawner : MonoBehaviour
{

    SpawnClusterContainer container;

    static string dataPath;

    SpawnInfo data;


    private void Awake()
    {
        TryGetComponent<SpawnClusterContainer>(out container);

        dataPath = $"{Application.streamingAssetsPath}/SpawnInfo.gdjs";


    }

    IEnumerator Start()
    {
        WebGlDataRead(dataPath);

        while (data is null)
        {
            yield return new WaitForEndOfFrame();
        }

        Debug.Log(data.GetSpawnData(0).ScId);

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

        }
    }
}
