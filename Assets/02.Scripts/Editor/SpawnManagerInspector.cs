//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;


//[CanEditMultipleObjects]//���߼����� �����ϰ���
//[CustomEditor(typeof(SpawnSystemManager))]
//public class SpawnManagerInspector : Editor
//{

//    SpawnSystemManager spawnSystemManger;
//    SerializedProperty spawnClusters;
//    private void OnEnable()
//    {
//        if (spawnSystemManger == null)
//        {
//            spawnSystemManger = (SpawnSystemManager)target;
//        }
//    }

//    public override void OnInspectorGUI()
//    {
//        //SerializedProperty(spawnClusters, "spawnClusters", "����Ŭ������");

//        for(int i=0; i< spawnSystemManger.spawnClusters.Count;++i)
//        {
//            //spawnSystemManger.spawnClusters[i].scId =  EditorGUILayout.IntField("Ŭ������ ID",);
//        }

//        if(GUILayout.Button("Ŭ������ �߰�"))
//        {
//            spawnSystemManger.spawnClusters.Add(new SpawnSystemManager.SpawnCluster());
//        }
//        //base.OnInspectorGUI();
//    }

//    void SerializedProperty(SerializedProperty property, string propertyName, string name)
//    {
//        property = serializedObject.FindProperty(propertyName);
//        EditorGUI.BeginChangeCheck();
//        EditorGUILayout.PropertyField(property, new GUIContent(name), true);
//        if (EditorGUI.EndChangeCheck())
//        {
//            serializedObject.ApplyModifiedProperties();
//        }
//    }
//}
