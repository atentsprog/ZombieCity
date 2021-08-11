using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    /// 특정시간마다 웨이브 진행
    /// 몬스터가 다 죽어도 다음 웨이브 진행
    public float nextWaveWaitTime = 3f;

    [System.Serializable]
    public class WaveInfo
    {
        public int spawnCount = 10;
        public float waveInterval = 60f;
        public List<SpawnInfo> spawnInfos;
    }
    public List<WaveInfo> waveInfo;

    [System.Serializable]
    public class SpawnInfo
    {
        public Zombie go;
        public float ratio;
    }


    public SpawnPoint[] spawnPoints;

    public float waveEndTime;
    private IEnumerator Start()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>(true);

        foreach(var item in waveInfo)
        {
            yield return SpawnWaveMonsterCo(item);
            waveEndTime = item.waveInterval + Time.time;
            while (waveEndTime > Time.time)
                yield return null;

            // 다음 웨이브 시작했습니다. <- 알림 UI 추가하자.
            Debug.Log("다음 웨이브 시작했습니다.");
            yield return new WaitForSeconds(nextWaveWaitTime);
        }

        Debug.Log("모든 웨이브가 진행되었습니다.");
    }

    public float randomSpawnDelay = 1f;
    private IEnumerator SpawnWaveMonsterCo(WaveInfo waveInfo)
    {
        int spawnCount = waveInfo.spawnCount;
        List<SpawnInfo> spawnInfos = waveInfo.spawnInfos;
        for (int i = 0; i < spawnCount; i++)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            var point = spawnPoints[randomIndex].transform.position;
            Zombie regenMonster = spawnInfos.OrderBy(x => Random.Range(0, x.ratio)).Last().go;
            Instantiate(regenMonster, point, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(0, randomSpawnDelay));
        }
    }
}
