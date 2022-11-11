using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Newtonsoft.Json;
public class SpawnGroupObj : MonoBehaviour
{



    public SpawnGroup spawnGroupData;

    float time;

    int lastSpawnIndex;

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
                    if (spawnGroupData.spawnRandom)
                    {
                        RandomSpawn();
                    }
                    else
                    {
                        Spawn();
                    }
                }

            }
        }
    }

    public void Spawn()
    {
        lastSpawnIndex %= spawnGroupData.Sp.Count;

        SpawnMonster(spawnGroupData.monsterType, spawnGroupData.Sp[lastSpawnIndex].spawnPoint);

        ++lastSpawnIndex;
    }

    public void RandomSpawn()
    {

        int rand = Random.Range(0, spawnGroupData.Sp.Count);

        SpawnMonster(spawnGroupData.monsterType, spawnGroupData.Sp[rand].spawnPoint);

        //for (int i = 0; i < group.maxCount; ++i)
        //{
        //    int rand = Random.Range(0, group.Sp.Count);

        //    SpawnMonster(group.monsterType, group.Sp[rand].spawnPoint);
        //}
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
