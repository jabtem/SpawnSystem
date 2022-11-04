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

    public SpawnClusterContainer spawnClusterContainer;

    [SerializeField] List<SpawnCluster> clusters;
    static string path;

    Editor editor;
    private void OnEnable()
    {
        spawnClusterContainer = GameObject.FindObjectOfType<SpawnClusterContainer>();
        clusters = spawnClusterContainer.spawnClusters;
        editor = Editor.CreateEditor(this);
    }

    private void OnInspectorUpdate()
    {
        Repaint();
    }

    private void OnGUI()
    {

        spawnClusterContainer = EditorGUILayout.ObjectField("스폰클러스터 스크립트", spawnClusterContainer, typeof(SpawnClusterContainer), true) as SpawnClusterContainer;
        path = Application.dataPath + "/Resources/Spawndata/SpawnData.json";

        if (GUILayout.Button("Json Save"))
        {
            JsonSave();
        }

        //if (spawnClusterContainer != null)
        //{
        //    if (editor)
        //    {
        //        editor.OnInspectorGUI();
        //    }
        //}

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
            File.WriteAllText(path, save);
            Debug.Log("Save Json!");

            AssetDatabase.Refresh();
        }
    }






}

[CustomEditor(typeof(SpawnDataGenerator))]
public class SpawnDataGeneratorDraw :Editor
{

    public override void OnInspectorGUI()
    {
        SerializedProperty("clusters", "스폰클러스터");

    }
    void SerializedProperty(string propertyName, string name)
    {
        var property = serializedObject.FindProperty(propertyName);
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(property, new GUIContent(name), true);
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}
