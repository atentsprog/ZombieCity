using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingWeapon : MonoBehaviour
{
    public Transform targetPoint;
    public float speed = 30;

    private void Awake()
    {
        if (firePoint == null)
            firePoint = transform;
    }
    void Update()
    {
        SetTargetWithSpeed(targetPoint.position, speed);

        //// 우클릭하면 수류탄 투척 바로 해보자.
        if (Input.GetKeyDown(KeyCode.Mouse1))
            Fire();
    }

    public Transform firePoint;
    public ProjectileArc projectileArc;
    public float currentAngle;
    public Vector3 currentDirection;
    public void SetTargetWithSpeed(Vector3 point, float speed)
    {
        currentDirection = point - firePoint.position;
        float yOffset = currentDirection.y;
        currentDirection = Math3d.ProjectVectorOnPlane(Vector3.up, currentDirection);
        float distance = currentDirection.magnitude;

        float angle0, angle1;
        bool targetInRange = ProjectileMath.LaunchAngle(speed, distance, yOffset, Physics.gravity.magnitude, out angle0, out angle1);

        if (targetInRange)
            currentAngle = angle0;

        projectileArc.UpdateArc(speed, distance, Physics.gravity.magnitude, currentAngle, currentDirection, targetInRange);
    }

    public Rigidbody projectilePrefab;
    public float cooldown = 1;
    public float lastShotTime;

    public void Fire()
    {
        if (Time.time > lastShotTime + cooldown)
        {
            Rigidbody p = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            float degree = currentAngle * Mathf.Rad2Deg;

            p.transform.forward = currentDirection;
            p.transform.Rotate(-degree, 0, -degree);
            p.velocity = p.transform.forward * speed;

            lastShotTime = Time.time;
            //anim.Rewind();
            //anim.Play();
        }
    }

}
