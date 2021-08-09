using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenScale: MonoBehaviour
{
    public float delay = 0;
    public float startScale = 0.7f;
    public float endScale = 1f;
    public float duration = 0.4f;
    public Ease ease = Ease.OutBounce;

    void OnEnable()
    {
        transform.localScale = Vector3.one * startScale;
        Sequence sequence = DOTween.Sequence();

        sequence.Insert(delay, transform.DOScale(startScale, 0));
        sequence.Insert(delay,
            transform.DOScale(endScale, duration)
                .SetEase(ease)
            ).SetLink(gameObject);
    }
}
