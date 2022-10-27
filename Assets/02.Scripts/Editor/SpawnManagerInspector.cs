using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CanEditMultipleObjects]//���߼����� �����ϰ���
[CustomEditor(typeof(SpawnSystemManager))]
public class SpawnManagerInspector : Editor
{

    SpawnSystemManager spawnSystemManger;
    SerializedProperty spawnClusters;



    //��ư�� Ŭ���� Ŭ�������� �ε���
    public int curIndex = -1;

    private void OnEnable()
    {
        if (spawnSystemManger == null)
        {
            spawnSystemManger = (SpawnSystemManager)target;
        }
    }

    public override void OnInspectorGUI()
    {
        SerializedProperty(spawnClusters, "spawnClusters", "����Ŭ������");
        SceneView view = GetSceneView();



        for (int i = 0; i < spawnSystemManger.spawnClusters.Count; ++i)
        {
            GUILayout.Label($"Spawn Cluster Num {i}");
            GUI.backgroundColor = Color.green;
            if (!spawnSystemManger.spawnClusters[i].isClick && GUILayout.Button("Point Set Start") )
            {
                curIndex = i;
                spawnSystemManger.spawnClusters[i].isClick = true;
                view.Focus();

            }
            GUI.backgroundColor = Color.red;
            if (spawnSystemManger.spawnClusters[i].isClick && GUILayout.Button("Point Set End"))
            {
                curIndex = -1;
                spawnSystemManger.spawnClusters[i].isClick = false;
            }

            //spawnSystemManger.spawnClusters[i].scId = EditorGUILayout.IntField("Ŭ������ ID", spawnSystemManger.spawnClusters[i].scId);
            //EditorGUILayout.BeginVertical();

            //EditorGUILayout.EndVertical();
        }
        GUI.backgroundColor = Color.white;
        if (GUILayout.Button("Ŭ������ �߰�"))
        {
            spawnSystemManger.spawnClusters.Add(new SpawnSystemManager.SpawnCluster());
        }

        //base.OnInspectorGUI();
    }

    public void OnSceneGUI()
    {




        if (curIndex >= 0 && spawnSystemManger.spawnClusters.Count > 0)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

            if (Event.current.type != EventType.MouseDown)
                return;

            if (Event.current.button == 0)
            {

                Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Debug.Log(hit.point);
                }
            }
        }


    }

    public static SceneView GetSceneView()
    {
        SceneView view = SceneView.lastActiveSceneView;
        if (view == null)
            view = EditorWindow.GetWindow<SceneView>();

        return view;
    }

    void SerializedProperty(SerializedProperty property, string propertyName, string name)
    {
        property = serializedObject.FindProperty(propertyName);
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(property, new GUIContent(name), true);
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}
