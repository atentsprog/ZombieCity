using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;

public class MoveToPlayer : MonoBehaviour
{
    NavMeshAgent agent;
    public float maxSpeed = 20;
    public float duration = 3;

    bool alreadyDone = false;

    private void OnTriggerEnter(Collider other)
    {
        if (alreadyDone)
            return; // 코루틴 정지

        if (other.CompareTag("Player"))
        {
            alreadyDone = true;
            agent = GetComponent<NavMeshAgent>();

            //var tweenResult = DOTween.To(() => agent.speed, (x) => agent.speed = x, maxSpeed, duration);
            //tweenResult.SetLink(gameObject);

            DOTween.To(() => agent.speed, (x) => agent.speed = x, maxSpeed, duration)
                .SetLink(gameObject);

            setDestinationCoHandle = StartCoroutine(SetDestinationCo(other.transform));
        }
    }

    public void StopCoroutine()
    {
        StopCoroutine(setDestinationCoHandle);
    }
    Coroutine setDestinationCoHandle;
    private IEnumerator SetDestinationCo(Transform tr)
    {
        while (true)
        {
            agent.destination = tr.position;
            yield return null;
        }
    }
}
