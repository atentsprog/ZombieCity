using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class MoveToPlayer : MonoBehaviour
{
    NavMeshAgent agent;
    public float maxSpeed = 20;
    public float duration = 3;

    bool alreadyDone = false;
    TweenerCore<float, float, FloatOptions> tweenResult;
    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (alreadyDone)
            yield break; // 코루틴 정지

        if (other.CompareTag("Player"))
        {
            alreadyDone = true;
            agent = GetComponent<NavMeshAgent>();
            tweenResult = DOTween.To(() => agent.speed, (x) => agent.speed = x, maxSpeed, duration);

            while(true)
            {
                agent.destination = other.transform.position;
                yield return null;
            }
        }
    }
    private void OnDestroy()
    {
        tweenResult.Kill();
    }
}
