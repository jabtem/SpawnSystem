using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class SpawnDataGenerator : EditorWindow
{

    [MenuItem("Tools/SpawnData")]
    public new static void Show()
    {
        SpawnDataGenerator wnd = GetWindow<SpawnDataGenerator>();
        wnd.titleContent = new GUIContent("spawnsystemtool ui");
    }

    SpawnClusterContainer spawnClusterContainer;
    static string path;
    private void OnEnable()
    {
        spawnClusterContainer = GameObject.FindObjectOfType<SpawnClusterContainer>();
    }

    private void OnGUI()
    {
        spawnClusterContainer = EditorGUILayout.ObjectField("����Ŭ������", spawnClusterContainer, typeof(SpawnClusterContainer), true) as SpawnClusterContainer;
        path = Application.dataPath + "/Resources/Spawndata/SpawnData.json";

        if (GUILayout.Button("Json Save"))
        {
            JsonSave();
        }
    }

    void JsonSave()
    {

        if (spawnClusterContainer != null)
        {

            spawnClusterContainer.spawnData.spawnClusters = spawnClusterContainer.spawnClusters;




            var save = JsonUtility.ToJson(spawnClusterContainer.spawnData, true);
            //var save = JsonConvert.SerializeObject(spawnClusterContainer.spawnClusters, Formatting.Indented);
            //var save = JsonConvert.SerializeObject(spawnClusterContainer.spawnClusters, Formatting.Indented, new JsonSerializerSettings
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //});
            Debug.Log(save);
            File.WriteAllText(path, save);
            Debug.Log("Save Json!");
        }
    }

}
