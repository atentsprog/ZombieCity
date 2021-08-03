using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Zombie : Actor
{
    public string debugLog;
    public Transform target;
    NavMeshAgent agent;
    float originalSpeed;

    IEnumerator Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        target = FindObjectOfType<Player>().transform;  // 
        originalSpeed = agent.speed;

        CurrentFsm = ChaseFSM;

        while (true) // 상태를 무한히 반복해서 실행하는 부분.
        {
            var previousFSM = CurrentFsm;

            fsmHandle = StartCoroutine(CurrentFsm());

            // FSM 안에서 에러 발생시 무한 루프 도는 것을 방지 하기 위해서 추가함.
            if (fsmHandle == null && previousFSM == CurrentFsm)
                yield return null;

            while (fsmHandle != null)
                yield return null;
        }
    }

    IEnumerator ChaseFSM()
    {
        agent.destination = target.position;

        float endWaitTime = Random.Range(0.5f, 2f);

        yield return new WaitForSeconds(endWaitTime);
        SetCurrentFsm_AttackOrChase();
    }

    private void SetCurrentFsm_AttackOrChase()
    {
        if (ExistAttackTarget() == false)
        {
            CurrentFsm = RoamingFSM;
        }
        else
        {
            if (GetTargetDistance() < attackRange)
                CurrentFsm = AttackFSM;
            else
                CurrentFsm = ChaseFSM;
        }
    }

    private bool ExistAttackTarget()
    {
        return target.GetComponent<Collider>().enabled;
    }

    public float roamingDistance = 5;
    // todo:랜덤한 지역을 천천히 이동.
    IEnumerator RoamingFSM()
    {
        SetNormalSpeed();
        while (true)
        {
            Vector3 randomPos = transform.position;
            randomPos.x += Random.Range(-roamingDistance, roamingDistance);
            randomPos.z += Random.Range(-roamingDistance, roamingDistance);
            animator.SetTrigger("Walk");
            agent.destination = randomPos;

            while (true)
            {
                yield return null;
                if (agent.velocity.magnitude < 0.1f)
                {
                    break;
                }

                yield return null;
            }
            // todo:주기적으로 공격 대상이 있는지 찾아보자.
        }
    }

    private void SetNormalSpeed()
    {
        agent.speed = originalSpeed;
    }

    private float GetTargetDistance()
    {
        return Vector3.Distance(target.position, transform.position);
    }

    public float attackTime = 1;
    public float attackAnimationTime = 2;
    public SphereCollider attackCollider;
    public LayerMask playerLayer;
    
    IEnumerator AttackFSM()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackTime);

        //공격 사전거리에 있는지 물리 체크
        Collider[] enemyColliders = Physics.OverlapSphere(
            attackCollider.transform.position
            , attackCollider.radius, playerLayer);

        foreach (var item in enemyColliders)
        {
            item.GetComponent<Player>().TakeHit(power);
        }

        float waitTime = attackAnimationTime - attackTime;
        yield return new WaitForSeconds(waitTime);

        SetCurrentFsm_AttackOrChase();
    }

    public float attackRange = 10;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    Coroutine fsmHandle;
    protected Func<IEnumerator> CurrentFsm
    {
        get { return m_currentFsm; }
        set
        {
            debugLog = $"{m_currentFsm} -> {value}";
            m_currentFsm = value;
            fsmHandle = null;
        }
    }
    Func<IEnumerator> m_currentFsm;


    IEnumerator TakeHitFSM()
    {
        //animator.Play("TakeHit");
        animator.Play(Random.Range(0, 2) == 0 ? "TakeHit1" : "TakeHit2", 0, 0);
        // 피격 이펙트 생성(피,..)
        CreateBloodEffect();


        // 이동 스피드를 잠시 0으로 만들자.
        agent.speed = 0;
        yield return new WaitForSeconds(TakeHitStopSpeedTime);
        SetNormalSpeed();

        if (hp <= 0)
        {
            GetComponent<Collider>().enabled = false;
            CurrentFsm = DieFSM;
        }
        else
            CurrentFsm = ChaseFSM;
    }
    IEnumerator DieFSM()
    {
        animator.Play("Die");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    public float TakeHitStopSpeedTime = 0.1f;
    public float moveBackDistance = 0.1f;
    public float moveBackNoise = 0.1f;
    private void PushBackMove(Vector3 toMoveDirection)
    {
        toMoveDirection.x += Random.Range(-moveBackNoise, moveBackNoise);
        toMoveDirection.z += Random.Range(-moveBackNoise, moveBackNoise);
        toMoveDirection.y = 0;
        toMoveDirection.Normalize();
        transform.Translate(toMoveDirection * moveBackDistance, Space.World);
    }

    public void TakeHit(int damage, Vector3 toMoveDirection)
    {
        base.TakeHit(damage);

        // 뒤로 밀려나게하자.
        PushBackMove(toMoveDirection);
        CurrentFsm = TakeHitFSM;
    }

}
