using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    /// 특정시간마다 웨이브 진행
    /// 몬스터가 다 죽어도 다음 웨이브 진행
    //public float waveInterval = 60f;
    //public float waveWaitTime = 3f;

    [System.Serializable]
    public class SpawnInfo
    {
        public Zombie go;
        public float ratio;
    }

    public List<SpawnInfo> spawnInfos;
    public int spawnCount = 10;

    public SpawnPoint[] spawnPoints;

    private IEnumerator Start()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>(true);
        for (int i = 0; i < spawnCount; i++)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            var point = spawnPoints[randomIndex].transform.position;
            Zombie regenMonster = spawnInfos.OrderBy(x => Random.Range(0, x.ratio)).Last().go;
            Instantiate(regenMonster, point, Quaternion.identity);
        }
        yield return null;
    }
}
