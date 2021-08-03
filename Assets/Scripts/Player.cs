﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        bulletLight = GetComponentInChildren<Light>(true).gameObject;
    }
    void Update()
    {
        if (Time.deltaTime == 0)
            return;

        if (stateType != StateType.Roll)
        {
            LookAtMouse();
            Move();
            Fire();
            Roll();
        }
    }

    private void Roll()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(RollCo());
    }

    public AnimationCurve rollingSpeedAC;

    public float rollingSpeedUserMultipy = 1;

    public enum StateType
    {
        Idle,
        Move,
        Attack,
        TakeHit,
        Roll,
        Die,
    }
    public StateType stateType = StateType.Idle;
    private IEnumerator RollCo()
    {
        animator.SetBool("Fire", false);
        DecreaseRecoil();

        // 회전 방향은 처음 바라보던 방향으로 고정.
        // 총알 금지. 움직이는거 금지.마우스 바라보는거 금지.
        stateType = StateType.Roll;
        // 구르는 애니메이션 재생.
        animator.SetTrigger("Roll");
        // 구르는 동안 이동 스피드를 빠르게 하기.
        float starTime = Time.time;
        float endTime = starTime + rollingSpeedAC[rollingSpeedAC.length - 1].time;
        while(endTime > Time.time)
        {
            float time = Time.time - starTime;
            float rollingSpeedMultipy = rollingSpeedAC.Evaluate(time) * rollingSpeedUserMultipy;
            //print($"{time}:{rollingSpeedMultipy} : {rollingSpeedAC[rollingSpeedAC.length - 1].time}");

            transform.Translate(transform.forward * speed * rollingSpeedMultipy * Time.deltaTime, Space.World);
            yield return null;
        }
        stateType = StateType.Idle;
    }

    Plane plane = new Plane(new Vector3(0, 1, 0), 0);

    private void LookAtMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (plane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            Vector3 dir = hitPoint - transform.position;
            dir.y = transform.position.y;
            dir.Normalize();
            transform.forward = dir;
        }
    }


    private void Move()
    {
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) move.z = 1;
        if (Input.GetKey(KeyCode.S)) move.z = -1;
        if (Input.GetKey(KeyCode.A)) move.x = -1;
        if (Input.GetKey(KeyCode.D)) move.x = 1;
        if (move != Vector3.zero)
        {
            Vector3 relateMove;
            relateMove = Camera.main.transform.forward * move.z;
            relateMove += Camera.main.transform.right * move.x;
            relateMove.y = 0;
            move = relateMove;

            move.Normalize();
            transform.Translate(move * speed * Time.deltaTime, Space.World);
        }


        animator.SetFloat("DirX", move.x);
        animator.SetFloat("DirY", move.z);
        animator.SetFloat("Speed", move.sqrMagnitude);
    }

    public float speed = 5;
}
