using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class MoveToPlayer : MonoBehaviour
{
    bool alreadyDone = false;
    NavMeshAgent agent;
    public float maxSpeed = 20;
    public float increaseSpeedDuration = 2;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    TweenerCore<float, float, FloatOptions> tweeningResult;
    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (alreadyDone)
            yield break;

        if (other.CompareTag("Player"))
        {
            alreadyDone = true;

            Transform target = other.transform;

            // 속도 점점 증가. 
            tweeningResult = DOTween.To(() => agent.speed, x => agent.speed = x, maxSpeed, increaseSpeedDuration);

            while (true)
            {
                agent.destination = target.position;                
                yield return null;
            }
        }
    }
    private void OnDestroy()
    {
        tweeningResult.Kill();
    }
}
