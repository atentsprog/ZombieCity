using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    public ProjectileArc projectileArc;
    public Transform firePoint;
    public Cursor cursor;
    public float speed = 20;
    void Start()
    {
        projectileArc = GetComponent<ProjectileArc>();
        firePoint = transform;
        //cursor = FindObjectOfType<Cursor>();
    }
    void Update()
    {
        SetTargetWithSpeed(cursor.transform.position, speed);

        //// 수류탄 발사
        //if (Input.GetKeyDown(KeyCode.Mouse1))
        //    ThrowObject();
    }

    public void ThrowObject()
    {
        //수류탄 생성.
        //리지드 바디에 포스를 줘서 날리자.
        var newGrenadeGo = Instantiate(grenadeGo, firePoint.position, Quaternion.identity);
        newGrenadeGo.transform.forward = direction;
        float degree = -currentAngle * Mathf.Rad2Deg;
        newGrenadeGo.transform.Rotate(degree, 0, degree);
        Rigidbody _rigidbody = newGrenadeGo.GetComponent<Rigidbody>();
        _rigidbody.velocity = newGrenadeGo.transform.forward * speed;
        _rigidbody.AddTorque(Random.Range(0, torgue), Random.Range(0, torgue), Random.Range(0, torgue));

        StartCoroutine(ProjectileArcOffAndOnCo());
    }

    public float offTime = 0.5f; // 다시 사용하는 딜레이 시간 만큼 꺼주자.
    private IEnumerator ProjectileArcOffAndOnCo()
    {
        var lineRenderer = GetComponentInChildren<LineRenderer>(); 
        lineRenderer.enabled = false;
        yield return new WaitForSeconds(offTime);
        lineRenderer.enabled = true;
    }

    public float torgue = 100;

    public GameObject grenadeGo;
    public float currentAngle;
    Vector3 direction;
    public void SetTargetWithSpeed(Vector3 point, float speed)
    {
        direction = point - firePoint.position;
        float yOffset = direction.y;
        direction = Math3d.ProjectVectorOnPlane(Vector3.up, direction);
        float distance = direction.magnitude;
        float angle0, angle1;
        bool targetInRange = ProjectileMath.LaunchAngle(speed, distance, yOffset, Physics.gravity.magnitude, out angle0, out angle1);
        if (targetInRange)
            currentAngle = angle0;
        projectileArc.UpdateArc(speed, distance, Physics.gravity.magnitude, currentAngle, direction, targetInRange);
    }
}
