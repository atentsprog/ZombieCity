﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Actor
{
    public GunInfo startGun;
    public GameObject currentGun;
    public Transform weaponRightHandPosition;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        //bulletLight = GetComponentInChildren<Light>(true).gameObject;
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>(true);

        CreateGun(startGun);
    }

    private void CreateGun(GunInfo gunInfo)
    {
        GunInfo newGunInfo = Instantiate(gunInfo, weaponRightHandPosition);
        currentGun = newGunInfo.gameObject;
        //currentGun.transform.SetPositionAndRotation(gunInfo.gameObject.transform.localPosition
            //, gunInfo.gameObject.transform.localRotation);
        bulletLight = newGunInfo.bulletLight;

        animator.runtimeAnimatorController = newGunInfo.animatorOverride;

        bulletPosition = newGunInfo.bulletPosition;
        bullet = newGunInfo.bullet;

        shootDelay = newGunInfo.delay;
    }

    void Update()
    {
        if (Time.deltaTime == 0)
            return;

        LookAtMouse();
        Move();
        Fire();
        Roll();
    }

    private void Roll()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(RollCo());
        }
    }

    float multiplySpeed = 1;
    public AnimationCurve rollSpeed;
    private IEnumerator RollCo()
    {
        animator.Play("Roll");
        float startTime = Time.time;
        float endTime = rollSpeed[rollSpeed.length - 1].time + startTime;
        while(endTime > Time.time)
        {
            float time = Time.time - startTime;
            multiplySpeed = rollSpeed.Evaluate(time);
            print($"{time} : {multiplySpeed}");
            yield return null;
        }
        multiplySpeed = 1;
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
            transform.Translate(move * speed * multiplySpeed * Time.deltaTime, Space.World);
        }


        animator.SetFloat("DirX", move.x);
        animator.SetFloat("DirY", move.z);
        animator.SetFloat("Speed", move.sqrMagnitude);
    }

    public float speed = 5;


    new public void TakeHit(int damage)
    {
        base.TakeHit(damage);

        CreateBloodEffect();

        animator.SetTrigger("TakeHit");

        meshRenderer.material.color = Color.red;
        Invoke(nameof(SetOriginalColor), takeHitColorChangeTime);

        if( hp <= 0)
        {
            SetDieState();
        }
    }

    private void SetDieState()
    {
        animator.SetTrigger("Die");
        GetComponent<Collider>().enabled = false;
        enabled = false;
    }

    void SetOriginalColor()
    {
        meshRenderer.material.color = Color.white;
    }
    public float takeHitColorChangeTime = 0.1f;
    SkinnedMeshRenderer meshRenderer;
}
