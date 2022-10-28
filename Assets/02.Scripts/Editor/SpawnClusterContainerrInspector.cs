using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CanEditMultipleObjects]//���߼����� �����ϰ���
[CustomEditor(typeof(SpawnClusterContainer))]
public class SpawnClusterContainerrInspector : Editor
{

    SpawnClusterContainer spawnSystemManger;
    SerializedProperty spawnClusters;



    //��ư�� Ŭ���� Ŭ�������� �ε���
    public int curIndex = -1;
    public SpawnGroup curSg;

    int pointIndex = 0;
    private void OnEnable()
    {
        if (spawnSystemManger == null)
        {
            spawnSystemManger = (SpawnClusterContainer)target;
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
                pointIndex = 0;
                GameObject group = new GameObject($"SpawnGroup");
                group.transform.SetParent(spawnSystemManger.transform);
                curSg = group.AddComponent<SpawnGroup>();
                curSg.Sp = new();
                curSg.scId = spawnSystemManger.spawnClusters[curIndex].scId;
                spawnSystemManger.spawnClusters[curIndex].Sg.Add(curSg);
                view.Focus();

                

            }
            GUI.backgroundColor = Color.red;
            if (spawnSystemManger.spawnClusters[i].isClick && GUILayout.Button("Point Set End"))
            {
                FinishEdit(i);

            }

            //spawnSystemManger.spawnClusters[i].scId = EditorGUILayout.IntField("Ŭ������ ID", spawnSystemManger.spawnClusters[i].scId);
            //EditorGUILayout.BeginVertical();

            //EditorGUILayout.EndVertical();
        }
        GUI.backgroundColor = Color.white;
        if (GUILayout.Button("Ŭ������ �߰�"))
        {
            spawnSystemManger.spawnClusters.Add(new SpawnClusterContainer.SpawnCluster());
        }

        //base.OnInspectorGUI();
    }
    

    void FinishEdit(int index)
    {
        if(index >=0)
        {
            spawnSystemManger.spawnClusters[index].isClick = false;
            curIndex = -1;

            if(curSg.Sp.Count ==0)
            {
                spawnSystemManger.spawnClusters[index].Sg.Remove(curSg);
                DestroyImmediate(curSg.gameObject);
                curSg = null;
            }
        }

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
                    GameObject go = new GameObject($"Point{pointIndex}");
                    go.transform.position = hit.point;
                    pointIndex++;
                    SpawnPoint sp = go.AddComponent<SpawnPoint>();
                    go.transform.SetParent(curSg.transform);
                    curSg.Sp.Add(sp);


                }
            }
        }


    }


    //�����Ͱ� ��Ŀ���� ������������ ȣ��
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
