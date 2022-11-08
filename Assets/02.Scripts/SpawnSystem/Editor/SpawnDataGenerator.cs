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
    Scene activeScene;
    static string path;

    private void OnEnable()
    {

        //clusters = spawnClusterContainer.spawnClusters;
        //editor = Editor.CreateEditor(this);
    }

    private void OnInspectorUpdate()
    {
        Repaint();
    }

    private void OnGUI()
    {




        if(activeScene != SceneManager.GetActiveScene())
        {
            activeScene = SceneManager.GetActiveScene();
        }

        spawnClusterContainer = EditorGUILayout.ObjectField("����Ŭ������ ��ũ��Ʈ", spawnClusterContainer, typeof(SpawnClusterContainer), true) as SpawnClusterContainer;
        path = Application.dataPath + $"/Resources/Spawndata/{activeScene.name}SpawnData.json";

        //Json ����
        if (GUILayout.Button("Json Save"))
        {
            JsonSave();
        }

        ////�����ͺ���(�̻���ڵ�)
        //if(GUILayout.Button("Recovery"))
        //{
        //    if(spawnClusterContainer is not null)
        //    {
        //        return;
        //    }

        //    Recovery();

        //}

        ///�����̳� ���翩�� üũ
        if(spawnClusterContainer == null)
        {
            if (GameObject.FindObjectOfType<SpawnClusterContainer>() != null)
            {
                spawnClusterContainer = GameObject.FindObjectOfType<SpawnClusterContainer>();
            }

            //�����̳� ������ ��ư ����
            if (GUILayout.Button("Create Container"))
            {

                Create();
            }
        }



    }

    void JsonSave()
    {

        if (spawnClusterContainer != null)
        {

            spawnClusterContainer.spawnData.spawnClusters = spawnClusterContainer.spawnClusters;



            var save = JsonUtility.ToJson(spawnClusterContainer.spawnData, true);

            //Json.Net ������ ����
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

    void Create()
    {
        GameObject newContainer = new GameObject("SpawnClusterContainer");
        spawnClusterContainer = newContainer.AddComponent<SpawnClusterContainer>();
        

    }





}

[CustomEditor(typeof(SpawnDataGenerator))]
public class SpawnDataGeneratorDraw :Editor
{

    public override void OnInspectorGUI()
    {
        SerializedProperty("clusters", "����Ŭ������");

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
