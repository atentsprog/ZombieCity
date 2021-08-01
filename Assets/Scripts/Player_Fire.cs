using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPosition;


    float shootDelayEndTime;
    void Fire()
    {
        if (Input.GetMouseButton(0))
        {
            if (shootDelayEndTime < Time.time)
            {
                animator.SetBool("Shooting", true);
                shootDelayEndTime = Time.time + shootDelay;
                IncreaseRecoil();
                Instantiate(bullet, bulletPosition.position, CalculateRecoil(transform.rotation));
            }
        }
        else
        {
            animator.SetBool("Shooting", false);
            DecreaseRecoil();
        }
    }



    float recoilValue = 0f;
    float recoilMaxValue = 1.5f;
    float recoilLerpValue = 0.1f;
    void IncreaseRecoil()
    {
        recoilValue = Mathf.Lerp(recoilValue, recoilMaxValue, recoilLerpValue);
    }
    void DecreaseRecoil()
    {
        recoilValue = Mathf.Lerp(recoilValue, 0, recoilLerpValue);

    }

    Vector3 recoil;
    Quaternion CalculateRecoil(Quaternion rotation)
    {
        recoil = new Vector3(Random.Range(-recoilValue, recoilValue), Random.Range(-recoilValue, recoilValue), 0);
        return Quaternion.Euler(rotation.eulerAngles + recoil);
    }

    [SerializeField] float shootDelay = 0.05f;
}
