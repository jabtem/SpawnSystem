using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Newtonsoft.Json;
public class SpawnGroupObj : MonoBehaviour
{



    public SpawnGroup spawnGroupData;

    float time;

    int spawnIndex;

    private void OnDrawGizmosSelected()
    {
        for (int i = 1; i < spawnGroupData.Sp.Count + 1; ++i)
        {
            Gizmos.color = Color.red;

            int j = i % spawnGroupData.Sp.Count;
            Gizmos.DrawLine(spawnGroupData.Sp[i - 1].spawnPoint, spawnGroupData.Sp[j].spawnPoint);
        }
    }

    private void OnValidate()
    {
        SpawnClusterContainer.Instance.DataRefresh();
    }

    private void Update()
    {


        if(spawnGroupData.spawnStart)
        {
            
            if(spawnGroupData.SpawnCount < spawnGroupData.maxCount)
            {
                time += Time.deltaTime;

                if (time >= spawnGroupData.spawnDelay)
                {

                    ++spawnGroupData.SpawnCount;
                    time = 0f;
                    Spawn();
                }

            }
        }
    }

    public void Spawn()
    {
        //�����׷��� �����ɼ��ΰ��
        if(spawnGroupData.spawnRandom)
        {
            spawnIndex = Random.Range(0, spawnGroupData.Sp.Count);
        }
        else
        {
            spawnIndex %= spawnGroupData.Sp.Count;
        }


        //��������Ʈ�� �����ɼ��ΰ��
        if (spawnGroupData.Sp[spawnIndex].isRandom)
        {

            //��������Ʈ�� �ݰ���� ���� ��ǥ 
            Vector3 originPoint = spawnGroupData.Sp[spawnIndex].spawnPoint;
            Vector3 randPoint = Random.insideUnitCircle * spawnGroupData.Sp[spawnIndex].radius;
            Vector3 randSpawnPoint = originPoint + randPoint;
            

            //y��ǥ ����
            randSpawnPoint.y = originPoint.y;
            SpawnMonster(spawnGroupData.monsterType, randSpawnPoint);
        }
        else
        {
            SpawnMonster(spawnGroupData.monsterType, spawnGroupData.Sp[spawnIndex].spawnPoint);
        }


        ++spawnIndex;
    }


    public void SpawnMonster(string type, Vector3 pos)
    {
        StartCoroutine(CoSpawnMonster(type, pos));
    }

    IEnumerator CoSpawnMonster(string type, Vector3 pos)
    {
        var handle = Addressables.InstantiateAsync(type);
        handle.Completed += (data) =>
        {
            data.Result.transform.position = pos;
            Creature group = data.Result.AddComponent<Creature>();
            group.parentSet(spawnGroupData);

        };

        yield return handle;
    }
}
