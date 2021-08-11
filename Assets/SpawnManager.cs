using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : SingletonMonoBehavior<SpawnManager>
{
    public int currentWaveIndex;

    [System.Serializable]
    public class WaveInfo
    {
        public int spawnCount = 10;
        public GameObject monster;
        public float time;
    }
    public List<WaveInfo> waves;

    float nextWaveStartTime;
    public void OnClearAllMonster()
    {
        nextWaveStartTime = 0;
    }
    IEnumerator Start()
    {
        var spawnPoints = GetComponentsInChildren<SpawnPoint>(true);
        foreach (var item in waves)
        {
            Debug.LogWarning($"{++currentWaveIndex} 시작됨");
            int spawnCount = item.spawnCount;
            for (int i = 0; i < spawnCount; i++)
            {
                int spawnIndex = Random.Range(0, spawnPoints.Length);
                Vector3 spawnPoint = spawnPoints[spawnIndex].transform.position;
                Instantiate(item.monster, spawnPoint, Quaternion.identity);
            }
            nextWaveStartTime = Time.time + item.time;

            // 웨이브 끝나는 시간이 되기전에 모든 몬스터 죽이면 다음 웨이브 시작되게 하자
            while (Time.time < nextWaveStartTime)
                yield return null;

            // 웨이브 바뀔때마다 밤 낮 바뀌게 하자.
            LightManager.Instance.ToggleLight();
        }
    }
}
