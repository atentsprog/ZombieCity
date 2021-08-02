using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    Animator animator;
    public int hp = 100;
    IEnumerator Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        target = FindObjectOfType<Player>().transform;  // 

        originalMoveSpeed = agent.speed;
        while (hp > 0)
        {
            if (target)
                agent.destination = target.position;
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        }
    }
    internal void TakeHit(int damage)
    {
        hp -= damage;
        animator.Play("TakeHit", 0, 0);
        agent.speed = 0;
        Invoke(nameof(RestoreSpeed), 0.2f);

        // 피격 이펙트 생성(피,..)
        CreateTakeHitEffect();

        if (hp <= 0)
        {
            CancelInvoke(nameof(RestoreSpeed));
            GetComponent<Collider>().enabled = false;
            Invoke(nameof(Die), dieAnimationPlayTime);
        }
    }
    public float dieAnimationPlayTime = 0.6f;

    float originalMoveSpeed;
    private void RestoreSpeed()
    {
        agent.speed = originalMoveSpeed;
    }
     
    public GameObject takeHitEffects;
    public float takeHitEffectsYOffset = 1f;
    private void CreateTakeHitEffect()
    {
        Instantiate(takeHitEffects, transform.position + new Vector3(0, takeHitEffectsYOffset, 0), Quaternion.identity);
    }

    public float dieDestroyTime = 3;
    void Die()
    {
        animator.Play("Die");
        Destroy(gameObject, dieDestroyTime);
    }
}
