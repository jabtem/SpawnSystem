//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;
//using System.IO;
//using Newtonsoft.Json;
//using UnityEngine.SceneManagement;

//[System.Serializable]
//public class SpawnCluster
//{
//    public List<SpawnGroup> Sg = new();
//    //This ID
//    //자기 자신 ID
//    public int scId;

//    //Explanation
//    //클러스터에대한 부가설명
//    public string explanation;


//    //클러스터가 추가된 씬네임
//    public string sceneName;

//}

//[System.Serializable]
//public class SpawnGroup
//{
//    public List<SpawnPoint> Sp = new();
//    //This ID
//    public int sgId;
//    //Parent ID
//    public int scId;
    
//    public string monsterType;
//    public int maxCount;
//}

//[System.Serializable]
//public class SpawnPoint
//{
//    //This ID
//    public int spId;
    
//    //Parent ID;
//    public int sgId;

//    //SpawnPoint
//    public Vector3 spawnPoint;


//    public int radius;

//    public bool isRandom;

//    public SpawnPoint(int _Spid, int _SgId, Vector3 point)
//    {
//        spId = _Spid;
//        sgId = _SgId;
//        spawnPoint = point;
//    }

//}

//[System.Serializable]
//public class SpwanSaveData
//{
//    //ID 중복검사는 Set으로 관리
//    public List<SpawnCluster> Sc = new();
//    //고유 id값 저장용
//    public HashSet<int> uniqueScId = new();
//    public HashSet<int> uniqueSgId = new();
//    public HashSet<int> uniqueSpId = new();
//}



//public class SpawnSystemTool : EditorWindow
//{


//    SpwanSaveData data;


//    static string path;



//    bool loadFile = false;

//    int scid;

//    //현재 포커스된 필드의 네임
//    string curFocusFieldName = "";
//    Scene activeScene;

//    [MenuItem("Tools/SpawnData")]
//    public new static void Show()
//    {
//        SpawnSystemTool wnd = GetWindow<SpawnSystemTool>();
//        wnd.titleContent = new GUIContent("SpawnSystemTool UI");
//    }
//    private void OnEnable()
//    {
//        data = new SpwanSaveData();
//        path = Application.dataPath + "/Resources/Spawndata/SpawnData.json";
//        activeScene = SceneManager.GetActiveScene();
//        if (File.Exists(path))
//        {
//            Debug.Log("Exist SpawnData");

//            var json = Resources.LoadAsync<TextAsset>("SpawnData/SpawnData");
//            //var data = JsonConvert.DeserializeObject<List<string>>(json.asset.ToString());

//            //foreach (var a in data)
//            //{
//            //    test.Add(a);
//            //}
//        }
//        else
//        {
//            Debug.Log("None SpawnData");
//        }

//    }
//    private void OnLostFocus()
//    {
//        Debug.Log("Lost");
//    }
//    private void OnGUI()
//    {

//        if(!ReferenceEquals(GUI.GetNameOfFocusedControl(),curFocusFieldName))
//        {
//            curFocusFieldName = GUI.GetNameOfFocusedControl();
//            Debug.Log(curFocusFieldName);
//        }

//        for (int i = 0; i < data.Sc.Count; ++i)
//        {

//            //data.ScDic[i]
//            GUILayout.Label($"Spawn Cluster Number : {i} ");


//            GUILayout.BeginHorizontal();
//            GUILayout.Label("스폰 클러스터 ID");
//            GUI.SetNextControlName("SCID");
//            scid = EditorGUILayout.IntField(scid);


//            if (GUILayout.Button("Id 저장"))
//            {
//                data.uniqueScId.Add(scid);
//                data.Sc[i].scId = scid;
//            }
//            GUILayout.EndHorizontal();


//            GUILayout.BeginVertical();

//            if (data.uniqueScId.Contains(scid))
//            {
//                EditorGUILayout.HelpBox("스폰 클러스터 ID가 중복됩니다 다른 ID를 입력하세요", MessageType.Error);
//            }
            
//            GUILayout.EndVertical();



//            GUILayout.BeginHorizontal();
//            GUILayout.Label("클러스터 설명");
//            data.Sc[i].explanation = GUILayout.TextField(data.Sc[i].explanation);
//            GUILayout.EndHorizontal();




//            data.Sc[i].sceneName = activeScene.name;


//            if(GUILayout.Button("Remove"))
//            {

//                if (data.uniqueScId.Contains(data.Sc[i].scId))
//                {
//                    data.uniqueScId.Remove(data.Sc[i].scId);
//                }

//                data.Sc.Remove(data.Sc[i]);
//            }
//        }


//        if (GUILayout.Button("Add Cluster"))
//        {
//            data.Sc.Add(new SpawnCluster());
//            //data.ScDic.Add(data.scIndex, new SpawnCluster());
//            //data.ScDic[data.scIndex].scId = data.scIndex;
//            //data.scIndex++;
//        }

//        if(GUILayout.Button("Json Save"))
//        {
//            var save = JsonConvert.SerializeObject(data, Formatting.Indented);
//            File.WriteAllText(path,save);
//            Debug.Log("Save Data!");
//        }
//    }

//}

