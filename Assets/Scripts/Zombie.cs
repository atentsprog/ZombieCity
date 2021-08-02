using System.Collections;
using System.Collections.Generic;
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

        while(hp > 0)
        {
            if (target)
                agent.destination = target.position;
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        }
    }

    internal void TakeHit(int damage)
    {
        hp -= damage;
        //animator.Play("TakeHit");
        animator.Play(Random.Range(0, 2) == 0 ? "TakeHit1" : "TakeHit2", 0, 0);
        // 피격 이펙트 생성(피,..)
        if (hp <= 0)
        {
            GetComponent<Collider>().enabled = false;
            Invoke(nameof(Die), 1);
        }
    }
    void Die()
    {
        animator.Play("Die");
        Destroy(gameObject, 1);
    }
}
