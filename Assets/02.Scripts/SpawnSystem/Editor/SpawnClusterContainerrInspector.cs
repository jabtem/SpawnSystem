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
    SpawnGroupObj curSg;
    
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
                group.transform.SetParent(spawnClusterContainer.spawnClusters[i].clusterObj.transform);
                curSg = group.AddComponent<SpawnGroupObj>();


                spawnClusterContainer.spawnClusters[curIndex].SgObj.Add(curSg);

                curSg.spawnGroupData = new(spawnClusterContainer.spawnClusters[curIndex].scId);
                spawnClusterContainer.spawnClusters[curIndex].Sg.Add(curSg.spawnGroupData);
                view.Focus();

                

            }
            GUI.backgroundColor = Color.red;
            if (spawnClusterContainer.spawnClusters[i].isClick && GUILayout.Button("Point Set End"))
            {
                FinishEdit(i);

            }

        }
        GUI.backgroundColor = Color.white;
        if (GUILayout.Button("클러스터 추가"))
        {
            spawnClusterContainer.spawnClusters.Add(new SpawnCluster());
            int index = spawnClusterContainer.spawnClusters.Count-1;
            GameObject go = new("SpawnCluster");
            go.transform.SetParent(spawnClusterContainer.transform);

            spawnClusterContainer.spawnClusters[index].clusterObj = go;

        }
        //인스펙터 값 변경시 값유지위함
        if (GUI.changed)
        {
            spawnClusterContainer.DataRefresh();
            EditorUtility.SetDirty(target);

        }
    }
    

    void FinishEdit(int index)
    {


        if(index >=0)
        {
            spawnClusterContainer.spawnClusters[index].isClick = false;
            curIndex = -1;
            if(curSg.spawnGroupData.Sp.Count ==0)
            {
                spawnClusterContainer.spawnClusters[index].Sg.Remove(curSg.spawnGroupData);
                spawnClusterContainer.spawnClusters[index].SgObj.Remove(curSg);
                DestroyImmediate(curSg.gameObject);
                curSg = null;
            }
        }
        EditorUtility.SetDirty(target);
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

                    curSg.spawnGroupData.SpObj.Add(sp);
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
