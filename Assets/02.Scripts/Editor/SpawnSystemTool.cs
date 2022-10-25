using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

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


    //클러스터가 추가된 씬네임
    public string sceneName;

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


public class SpwanSaveData
{

}



public class SpawnSystemTool : EditorWindow
{



    Dictionary<int, SpawnCluster> ScDic = new();

    static string path;


    //데이터 로드후 시작 인덱스
    int scIndex = 0;
    int sgIndex = 0;
    int spIndex = 0;
    bool loadFile = false;


    Scene activeScene;

    [MenuItem("Tools/SpawnData")]
    public new static void Show()
    {
        SpawnSystemTool wnd = GetWindow<SpawnSystemTool>();
        wnd.titleContent = new GUIContent("SpawnSystemTool UI");
    }
    private void OnEnable()
    {
        path = Application.dataPath + "/Resources/Spawndata/SpawnData.json";
        activeScene = SceneManager.GetActiveScene();
        if (File.Exists(path))
        {
            Debug.Log("Exist SpawnData");

            //var json = Resources.LoadAsync<TextAsset>("SpawnData/SpawnData");
            //var data = JsonConvert.DeserializeObject<List<string>>(json.asset.ToString());

            //foreach (var a in data)
            //{
            //    test.Add(a);
            //}
        }
        else
        {
            Debug.Log("None SpawnData");
        }

    }
    private void OnGUI()
    {
        for (int i = 0; i < ScDic.Count; ++i)
        {
            
            ScDic[i].explanation = GUILayout.TextField(ScDic[i].explanation);
            ScDic[i].sceneName = activeScene.name;
        }


        if (GUILayout.Button("Add Cluster"))
        {
            ScDic.Add(scIndex, new SpawnCluster());
            ScDic[scIndex].scId = scIndex;
            scIndex++;
        }

        if(GUILayout.Button("Json Save"))
        {
            var data = JsonConvert.SerializeObject(ScDic,Formatting.Indented);
            File.WriteAllText(path,data);
            Debug.Log("Save Data!");
        }
    }


}

