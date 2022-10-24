//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;


//public class SpawnCluster
//{
//    public List<SpawnGroup> Sg = new();
//    //This ID
//    public int ScId;
//}

//public class SpawnGroup
//{
//    public List<SpawnPoint> Sp = new();
//    //This ID
//    public int SgId;
//    //Parent ID
//    public int ScId;
//}

//public class SpawnPoint
//{
//    //This ID
//    public int SpId;

//    //Parent ID;
//    public int SgId;
//}


//public class SpawnSystemTool : EditorWindow
//{

//    public Dictionary<int, SpawnPoint> SpDic = new();
//    public Dictionary<int, SpawnGroup> SgDic = new();
//    public Dictionary<int, SpawnCluster> ScDic = new();

//    [MenuItem("Tools/SpawnSystem")]
//    public new static void Show()
//    {
//        SpawnSystemTool wnd = GetWindow<SpawnSystemTool>();
//        wnd.titleContent = new GUIContent("SpawnSystemTool UI");
//    }

//}
