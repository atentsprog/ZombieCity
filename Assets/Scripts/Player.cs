using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    Animator animator;
    public float speed = 5;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        bulletLight = GetComponentInChildren<Light>(true).gameObject;
    }

    Plane plane = new Plane(new Vector3(0, 1, 0), 0);
    void Update()
    {
        if (Time.deltaTime == 0)
            return;

        Move();

        LookAt();

        Fire();
    }

    private void LookAt()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 dir = Vector3.zero;
        if (plane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            dir = hitPoint - transform.position;
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
            move.Normalize();
            transform.Translate(move * speed * Time.deltaTime, Space.World);
        }

        animator.SetFloat("Speed", move.sqrMagnitude);

        Vector2 direction2D = new Vector2(move.x, move.z);
        float angleY = VectorToDegree(direction2D);
        var rotation = (Quaternion.Euler(new Vector3(0, angleY, 0)) * Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up)).eulerAngles;

        var rotationY = rotation.y; 
        float radianX = Mathf.Cos(rotationY * Mathf.Deg2Rad);
        float radianY = Mathf.Sin(rotationY * Mathf.Deg2Rad);
        animator.SetFloat("DirX", radianX);
        animator.SetFloat("DirY", radianY);
    }
    public static float VectorToDegree(Vector2 vector) { 
        float radian = Mathf.Atan2(vector.y, vector.x); 
        return (radian * Mathf.Rad2Deg); }

}
