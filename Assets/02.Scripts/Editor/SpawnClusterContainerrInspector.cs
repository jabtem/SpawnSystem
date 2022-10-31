using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

using System.IO;

[CanEditMultipleObjects]//다중선택을 가능하게함
[CustomEditor(typeof(SpawnClusterContainer))]
public class SpawnClusterContainerrInspector : Editor
{

    SpawnClusterContainer spawnClusterContainer;
    SerializedProperty spawnClusters;



    //버튼을 클릭한 클러스터의 인덱스
    public int curIndex = -1;
    public SpawnGroupObj curSg;

    int pointIndex = 0;
    private void OnEnable()
    {
        if (spawnClusterContainer == null)
        {
            spawnClusterContainer = (SpawnClusterContainer)target;
        }
    }

    public override void OnInspectorGUI()
    {
        SerializedProperty(spawnClusters, "spawnClusters", "스폰클러스터");
        SceneView view = GetSceneView();



        for (int i = 0; i < spawnClusterContainer.spawnClusters.Count; ++i)
        {
            GUILayout.Label($"Spawn Cluster Num {i}");
            GUI.backgroundColor = Color.green;
            if (!spawnClusterContainer.spawnClusters[i].isClick && GUILayout.Button("Point Set Start") )
            {
                curIndex = i;
                spawnClusterContainer.spawnClusters[i].isClick = true;
                pointIndex = 0;
                GameObject group = new GameObject($"SpawnGroup");
                group.transform.SetParent(spawnClusterContainer.transform);
                curSg = group.AddComponent<SpawnGroupObj>();
                curSg.spawnGroupData = new(spawnClusterContainer.spawnClusters[curIndex].scId);
                //curSg.spawnGroupData.Sp = new();
                //curSg.spawnGroupData.scId = spawnClusterContainer.spawnClusters[curIndex].scId;
                spawnClusterContainer.spawnClusters[curIndex].Sg.Add(curSg.spawnGroupData);
                view.Focus();

                

            }
            GUI.backgroundColor = Color.red;
            if (spawnClusterContainer.spawnClusters[i].isClick && GUILayout.Button("Point Set End"))
            {
                FinishEdit(i);

            }

            //spawnSystemManger.spawnClusters[i].scId = EditorGUILayout.IntField("클러스터 ID", spawnSystemManger.spawnClusters[i].scId);
            //EditorGUILayout.BeginVertical();

            //EditorGUILayout.EndVertical();
        }
        GUI.backgroundColor = Color.white;
        if (GUILayout.Button("클러스터 추가"))
        {
            spawnClusterContainer.spawnClusters.Add(new SpawnCluster());
        }

        //base.OnInspectorGUI();
    }
    

    void FinishEdit(int index)
    {
        if(index >=0)
        {
            spawnClusterContainer.spawnClusters[index].isClick = false;
            curIndex = -1;
            Debug.Log(curSg.spawnGroupData.Sp);
            if(curSg.spawnGroupData.Sp.Count ==0)
            {
                spawnClusterContainer.spawnClusters[index].Sg.Remove(curSg.spawnGroupData);
                DestroyImmediate(curSg.gameObject);
                curSg = null;
            }
        }

    }

    public void OnSceneGUI()
    {




        if (curIndex >= 0 && spawnClusterContainer.spawnClusters.Count > 0)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

            if (Event.current.type != EventType.MouseDown)
                return;

            if (Event.current.button == 0)
            {



                Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    GameObject go = new GameObject($"Point{pointIndex}");
                    go.transform.position = hit.point;
                    pointIndex++;
                    SpawnPointObj sp = go.AddComponent<SpawnPointObj>();
                    go.transform.SetParent(curSg.transform);
                    sp.spawnPointData = new(curSg.spawnGroupData.sgId, go.transform.position);
                    curSg.spawnGroupData.Sp.Add(sp.spawnPointData);

                }
            }
        }


    }


    //에디터가 포커스가 빠져나갔을때 호출
    private void OnDisable()
    {
        FinishEdit(curIndex);
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
